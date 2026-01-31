using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private Shooter shooter;

    public NavMeshAgent Agent { get => agent; }

    public void Tick(Transform player)
    {
        agent.SetDestination(player.position);

        if(shooter != null)
        {
            shooter.Tick(player.position);
        }
    }
}
