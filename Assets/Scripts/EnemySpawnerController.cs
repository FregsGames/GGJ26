using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector.Editor.Validation;
using Sirenix.Utilities;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawnerController : MonoBehaviour, IController, ITickeable
{
    [SerializeField]
    private List<Enemy> enemyPrefabs;
    [SerializeField]
    private List<Transform> spawnPoint;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float spawnRate;
    [SerializeField]
    private int maxEnemyCount;

    private List<Enemy> enemies;

    private float timeSinceLastSpawn;

    public UniTask Prepare()
    {
        enemies = new List<Enemy>();
        timeSinceLastSpawn = spawnRate;
        return UniTask.CompletedTask;
    }

    public UniTask Setup()
    {
        return UniTask.CompletedTask;
    }

    public void Tick()
    {
        foreach (var enemy in enemies)
        {
            enemy.Tick(player);
        }

        if(timeSinceLastSpawn >= spawnRate && enemies.Count < maxEnemyCount)
        {
            SpawnEnemy();
        }
        else
        {
            timeSinceLastSpawn += Time.deltaTime;
        }
    }

    private void SpawnEnemy()
    {
        timeSinceLastSpawn = 0;

        var index = Random.Range(0, enemyPrefabs.Count - 1);

        var enemy = Instantiate(enemyPrefabs[index], CloserSpawnPointToPlayer(), Quaternion.identity);
        enemy.Agent.updateRotation = false;
        enemy.Agent.updateUpAxis = false;
        enemies.Add(enemy);
    }

    private Vector3 CloserSpawnPointToPlayer()
    {
        var s = spawnPoint.OrderBy(s => Vector3.Distance(s.position, player.position)).First();
        return s.position;
    }
}
