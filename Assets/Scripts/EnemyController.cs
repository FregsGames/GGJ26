using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, IController
{
    [SerializeField]
    private Transform player;
    public UniTask Prepare()
    {
        return UniTask.CompletedTask;
    }

    public UniTask Setup()
    {
        return UniTask.CompletedTask;
    }
   
}
