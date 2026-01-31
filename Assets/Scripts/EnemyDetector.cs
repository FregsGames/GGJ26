using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyDetector : MonoBehaviour, IController, ITickeable
{
    [SerializeField]
    private PlayerHealthController healthController;

    private List<EnemyData> enemies;

    public PlayerHealthController PlayerHealthController { get => healthController; }

    public UniTask Prepare()
    {
        enemies = new List<EnemyData>();
        return UniTask.CompletedTask;
    }

    public UniTask Setup()
    {
        return UniTask.CompletedTask;
    }

    public void Tick()
    {
        var toClear = new List<string>();

        foreach (var e in enemies)
        {
            if (e.enemy == null || e.enemy.gameObject == null)
            {
                toClear.Add(e.id);
            }
            else
            {
                e.remainingTime -= Time.deltaTime;

                if (e.remainingTime <= 0)
                {
                    e.remainingTime = .5f;
                    healthController.ReceiveDamage(e.enemy.CollisionDamage);
                }
            }
        }

        enemies = enemies.Where(e => !toClear.Contains(e.id)).ToList();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            var enemy = collision.gameObject.GetComponent<Enemy>();

            if (!HasEnemy(enemy))
            {
                enemies.Add(new EnemyData(Guid.NewGuid().ToString(), enemy, .5f));
            }
        }
    }

    private bool HasEnemy(Enemy enemy)
    {
        return enemies.FirstOrDefault(e => e.enemy == enemy) != null;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            var enemy = collision.gameObject.GetComponent<Enemy>();

            if (HasEnemy(enemy))
            {
                enemies = enemies.Where(e => e.enemy != enemy).ToList();
            }
        }
    }
}


public class EnemyData
{
    public string id;
    public Enemy enemy;
    public float remainingTime;

    public EnemyData(string id, Enemy enemy, float remainingTime)
    {
        this.id = id;
        this.enemy = enemy;
        this.remainingTime = remainingTime;
    }
}