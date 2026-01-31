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

    public NavMeshAgent Agent { get => agent; }
    public float CollisionDamage { get => collisionDamage; }
    private float remainingHealth;

    public void Setup()
    {
        remainingHealth = health;
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;

        if (enemyAnimation != null)
        {
            enemyAnimation.Setup();
        }
    }

    public bool Damage(float damage)
    {
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
        if(enemyAnimation != null)
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
}
