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

    public NavMeshAgent Agent { get => agent; }

    public void Setup()
    {
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;

        if (enemyAnimation != null)
        {
            enemyAnimation.Setup();
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
