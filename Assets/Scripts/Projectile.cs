using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private string targetTag;

    public async UniTask Move(float timeToLive, Vector3 direction, float speed, string targetTag)
    {
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
                PlayerHealthController.Instance.ReceiveDamage(5);
            }
            else
            {
                EnemySpawnerController.Instance.DamageEnemy(collision.GetComponent<Enemy>(), 5);
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
