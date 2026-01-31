using Unity.Cinemachine;
using UnityEngine;

public class ShootController : MonoBehaviour, ITickeable
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Transform shotPos;
    [SerializeField]
    private Projectile projectilePrefab;
    [SerializeField]
    private Transform shotTarget;
    [SerializeField]
    private MaskController maskController;
    [SerializeField]
    private float baseDamage;
    [SerializeField]
    private CinemachineImpulseSource impulseSource;

    private Vector3 direction;

    public void Tick()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;

        direction = (pos - shotPos.position).normalized;

        shotTarget.position = player.transform.position + direction * 3;
    }

    public void Shot()
    {
        if (Time.timeScale < 1)
            return;

        var p = Instantiate(projectilePrefab, shotPos.position, Quaternion.identity);
        _ = p.Move(maskController.Current == "mask.damage"? baseDamage * 3 : baseDamage, 5, direction, 10, "Enemy");

        if (OptionsController.Instance.Shake)
        {
            impulseSource.GenerateImpulse();
        }
    }
}
