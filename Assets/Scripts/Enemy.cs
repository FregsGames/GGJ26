using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.PlayerSettings;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private Shooter shooter;

    public NavMeshAgent Agent { get => agent; }

    public void Tick(Transform player)
    {
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
