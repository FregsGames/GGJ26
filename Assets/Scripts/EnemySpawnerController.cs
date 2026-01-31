using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
    [SerializeField]
    private MaskController maskController;
    [SerializeField]
    private PlayerHealthController playerHealthController;

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

        if (timeSinceLastSpawn >= spawnRate && enemies.Count < maxEnemyCount)
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

        var index = Random.Range(0, enemyPrefabs.Count);

        var enemy = Instantiate(enemyPrefabs[index], CloserSpawnPointToPlayer(), Quaternion.identity);
        enemy.Setup();

        enemies.Add(enemy);
    }

    private Vector3 CloserSpawnPointToPlayer()
    {
        var s = spawnPoint.OrderBy(s => Vector3.Distance(s.position, player.position)).First();
        return s.position;
    }

    public void DamageEnemy(Enemy enemy,float damage)
    {
        if (enemies.Contains(enemy))
        {
            bool dead = enemy.Damage(damage);

            if (dead)
            {
                enemies.Remove(enemy);
                enemy.Kill();
            }

            if(maskController.Current == "mask.life")
            {
                playerHealthController.Heal(damage * 0.5f);
            }
        }
    }
}
