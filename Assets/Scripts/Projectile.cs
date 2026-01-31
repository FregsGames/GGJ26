using Cysharp.Threading.Tasks;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private string targetTag;
    private float damage;

    public async UniTask Move(float damage, float timeToLive, Vector3 direction, float speed, string targetTag)
    {
        this.damage = damage;
        this.targetTag = targetTag;

        var elapsedTime = .0f;

        while (elapsedTime < timeToLive)
        {
            if (this == null)
                return;

            transform.position += direction * Time.deltaTime * speed;
            elapsedTime = Time.deltaTime;
            await UniTask.Yield();
        }

        if (this == null)
            return;

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == targetTag)
        {
            if(targetTag == "Player")
            {
                collision.GetComponentInChildren<EnemyDetector>().PlayerHealthController.ReceiveDamage(damage);
            }
            else
            {
                FindFirstObjectByType<EnemySpawnerController>().DamageEnemy(collision.GetComponent<Enemy>(), damage);
            }

            Destroy(gameObject);
        }
        else

        if (collision.gameObject.tag == "Obstacle")
        {
            Destroy(gameObject);
        }
    }
}
