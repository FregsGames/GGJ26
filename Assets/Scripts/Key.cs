using DG.Tweening;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]
    private string key;
    [SerializeField]
    private Collider2D col;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            col.enabled = false;
            FindFirstObjectByType<KeyController>().ObtainKey(key);
            transform.DOScale(0, 0.25f);
            transform.DOMove(collision.transform.position, 0.25f);
            Destroy(gameObject, 0.3f);
        }
    }
}
