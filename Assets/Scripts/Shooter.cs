using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    private Projectile projectilePrefab;
    [SerializeField]
    private float shotRate;

    private float timeSinceLastShot;

    public void Tick(Vector3 pos)
    {
        if(timeSinceLastShot < shotRate)
        {
            timeSinceLastShot += Time.deltaTime;
        }
        else
        {
            Debug.Log("shot");
            timeSinceLastShot = 0;
            var p = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            _=p.Move(5, (pos - transform.position).normalized, 10);
        }
    }
}
