using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;

    public NavMeshAgent Agent { get => agent; }

    public void Tick()
    {

    }
}
