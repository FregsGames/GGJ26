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
        var p = Instantiate(projectilePrefab, shotPos.position, Quaternion.identity);
        _ = p.Move(5, direction, 10, "Enemy");
    }
}
