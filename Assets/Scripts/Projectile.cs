using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public async UniTask Move(float timeToLive, Vector3 direction, float speed)
    {
        var elapsedTime = .0f;

        while (elapsedTime < timeToLive)
        {
            if (gameObject == null)
                return;

            transform.position += direction * Time.deltaTime * speed;
            elapsedTime = Time.deltaTime;
            await UniTask.Yield();
        }

        if (gameObject == null)
            return;

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerHealthController.Instance.ReceiveDamage(5);
            Destroy(gameObject);
        }
        else

        if (collision.gameObject.tag == "Obstacle")
        {
            Destroy(gameObject);
        }
    }
}
