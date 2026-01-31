using Unity.Cinemachine;
using Unity.VisualScripting;
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
    [SerializeField]
    private ExpController expController;
    [SerializeField]
    private UpgradesController upgradesController;

    [SerializeField]
    private float baseShotRate;
    [SerializeField]
    private float minShotRate;

    public float CurrentShotRate { get => Mathf.Clamp(baseShotRate - expController.Level * 0.1f, minShotRate, baseShotRate); }

    private float timeSinceLastShot;
    private Vector3 direction;

    public void Tick()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;

        direction = (pos - shotPos.position).normalized;

        shotTarget.position = player.transform.position + direction * 3;

        if(timeSinceLastShot < CurrentShotRate)
        {
            timeSinceLastShot += Time.deltaTime;
        }
        else
        {
            timeSinceLastShot = 0;
            Shot();
        }
    }

    public void Shot()
    {
        if (Time.timeScale < 1)
            return;

        var count = 1 + upgradesController.Level("upgrade.shotPoints");
        float angleStep = 360f / count;

        for (int i = 0; i < count; i++)
        {
            float angle = angleStep * i;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            var d = rotation* direction.normalized;

            var p = Instantiate(projectilePrefab, shotPos.position, Quaternion.identity);
            _ = p.Move(maskController.Current == "mask.damage" ? baseDamage * 3 : baseDamage, 5, d, 10, "Enemy");
        }


        if (OptionsController.Instance.Shake)
        {
            impulseSource.GenerateImpulse();
        }
    }
}
