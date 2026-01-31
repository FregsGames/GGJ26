using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour, IController, ITickeable
{
    [SerializeField]
    private List<Enemy> enemyPrefabs;
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

        var enemy = Instantiate(enemyPrefabs[index], GetSpawnPoint(), Quaternion.identity);
        enemy.Setup();

        enemies.Add(enemy);
    }

    private Vector3 GetSpawnPoint()
    {
        var playerPos = player.transform.position;

        var direction = new Vector3(Random.value, Random.value).normalized;

        while (direction.magnitude == 0)
        {
            direction = new Vector3(Random.value, Random.value).normalized;
        }


        return playerPos + direction * 10;
    }

    public void DamageEnemy(Enemy enemy, float damage)
    {
        if (enemies.Contains(enemy))
        {
            FindFirstObjectByType<ExplosionController>().Explosion(enemy.transform.position, 15, Color.white, 0.35f);

            bool dead = enemy.Damage(damage);

            if (dead && enemy.Explode)
            {
                FindFirstObjectByType<ExplosionController>().Explosion(enemy.transform.position, 15, Color.red, 2f);
            }

            if (dead)
            {
                enemies.Remove(enemy);
                _ = enemy.Kill();
            }

            if (maskController.Current == "mask.life")
            {
                playerHealthController.Heal(damage * 0.5f);
            }
        }
    }
}
