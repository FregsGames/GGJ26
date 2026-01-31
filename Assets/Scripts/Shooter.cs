using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    private Projectile projectilePrefab;
    [SerializeField]
    private float shotRate;
    [SerializeField]
    private float shotDistance;

    private float timeSinceLastShot;

    public float DistanceToShot { get => shotDistance; }

    public void Tick(Vector3 pos)
    {
        if (timeSinceLastShot < shotRate)
        {
            timeSinceLastShot += Time.deltaTime;
        }
        else
        {
            if (Vector3.Distance(transform.position, pos) < shotDistance)
            {
                timeSinceLastShot = 0;
                var p = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                _ = p.Move(5, (pos - transform.position).normalized, 10);
            }
        }
    }
}
