using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private Shooter shooter;
    [SerializeField]
    private EnemyAnimation enemyAnimation;
    [SerializeField]
    private float health;
    [SerializeField]
    private float collisionDamage;
    [SerializeField]
    private bool explode;
    [SerializeField]
    private float exp;
    [SerializeField]
    private List<Sprite> explodeAnimation;

    public bool dead;

    public NavMeshAgent Agent { get => agent; }
    public float CollisionDamage { get => collisionDamage; }
    public float Exp { get => exp; }
    public bool Explode { get => explode; }
    private float remainingHealth;

    public void Setup(int playerLevel)
    {
        remainingHealth = health + playerLevel * 2;
        agent.speed = agent.speed + playerLevel * 0.1f;
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;

        if (enemyAnimation != null)
        {
            enemyAnimation.Setup();
        }
    }

    public bool Damage(float damage)
    {
        if (dead)
            return false;

        if(enemyAnimation != null)
        {
            enemyAnimation.Blink();
        }

        remainingHealth -= damage;

        if(remainingHealth <= 0 )
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Tick(Transform player)
    {
        if (dead)
            return;

        if (enemyAnimation != null)
        {
            enemyAnimation.Tick(agent, player);
        }

        if(shooter != null)
        {
            shooter.Tick(player.position);
            var targetPos = player.position  + (transform.position - player.position).normalized * shooter.DistanceToShot * 0.75f;

            if(Vector3.Distance(transform.position, player.position) > shooter.DistanceToShot)
            {
                agent.SetDestination(targetPos);
            }
        }
        else
        {
            agent.SetDestination(player.position);
        }
    }

    public async UniTask Kill()
    {
        agent.isStopped = true;
        GetComponent<Collider2D>().enabled = false;
        if (explodeAnimation != null && explodeAnimation.Count > 0)
        {
            await enemyAnimation.DoOnce(explodeAnimation);
        }

        await transform.DOScale(0, .5f).AsyncWaitForCompletion();
        Destroy(gameObject);
    }
}
