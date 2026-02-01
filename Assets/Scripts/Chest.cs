using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField]
    private string requiredKey;
    [SerializeField]
    private AnimationOnce animationOnce;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (FindFirstObjectByType<KeyController>().HasKey(requiredKey))
            {
                GetComponent<Collider2D>().enabled = false;
                animationOnce.Animate();
            }
        }
    }
}
