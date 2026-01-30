using Cysharp.Threading.Tasks;
using Sirenix.Utilities.Editor;
using UnityEngine;

public class CameraController : MonoBehaviour, IController, ITickeable
{
    [SerializeField]
    private Transform cam;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float maxDistance;
    [SerializeField]
    private float minCamSpeed;

    private Vector3 goal;
    private Vector3 offset;
    private Vector3 lastPlayerPos;

    public void Tick()
    {
        var newPlayerPos = player.position;
        var playerDirection = (newPlayerPos - lastPlayerPos).normalized;



        var distanceToGoal = Vector3.Distance(cam.position, goal);



        lastPlayerPos = newPlayerPos;
    }

    public UniTask Prepare()
    {
        lastPlayerPos = player.position;

        offset = cam.position - player.position;
        return UniTask.CompletedTask;
    }

    public UniTask Setup()
    {
        return UniTask.CompletedTask;
    }
}
