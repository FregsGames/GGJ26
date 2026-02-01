using Cysharp.Threading.Tasks;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField]
    private string requiredKey;
    [SerializeField]
    private AnimationOnce animationOnce;

    private async void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (FindFirstObjectByType<KeyController>().HasKey(requiredKey))
            {
                AudioController.Instance.Play(Audios.Clip.Chest);
                GetComponent<Collider2D>().enabled = false;
                FindFirstObjectByType<MaskController>().ObtainShiny(requiredKey);
                animationOnce.Animate();
                await UniTask.Delay(1000);
                string desc = requiredKey + ".desc";
                string n = requiredKey + ".name";
                _=ConfirmationController.Instance.AskForConfirmation($"{"maskObtained".Localize()} {n.Localize()}\n{desc.Localize()}", "Ok", "Nice");
            }
        }
    }
}
