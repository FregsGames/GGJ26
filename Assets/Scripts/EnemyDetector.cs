using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [SerializeField]
    private PlayerHealthController healthController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            healthController.ReceiveDamage(5);
        }
    }
}
