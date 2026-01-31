using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    private Projectile projectilePrefab;
    [SerializeField]
    private float shotRate;
    [SerializeField]
    private float shotDistance;
    [SerializeField]
    private List<Sprite> shootAnimation;
    [SerializeField]
    private EnemyAnimation enemyAnimation;
    [SerializeField]
    private Transform shotPos;

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
                enemyAnimation.DoOnce(shootAnimation);
                timeSinceLastShot = 0;
                Shot(300, pos);
            }
        }
    }

    private async void Shot(int delay, Vector3 pos)
    {
        await UniTask.Delay(delay);

        if(this == null || gameObject == null || enemyAnimation == null)
        {
            return;
        }

        var p = Instantiate(projectilePrefab, shotPos.position, Quaternion.identity);
        _ = p.Move(5, 5, (pos - transform.position).normalized, 10, "Player");
    }
}
