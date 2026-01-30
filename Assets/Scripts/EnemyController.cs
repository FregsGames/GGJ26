using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, IController
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private NavMeshAgent enemyTest;

    public UniTask Prepare()
    {
        return UniTask.CompletedTask;
    }

    public UniTask Setup()
    {
        enemyTest.updateRotation = false;
        enemyTest.updateUpAxis = false;
        return UniTask.CompletedTask;
    }
    private void Update()
    {
        enemyTest.SetDestination(player.position);
    }
}
