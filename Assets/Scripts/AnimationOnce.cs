using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class AnimationOnce : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sRenderer;
    [SerializeField]
    private List<Sprite> sprites;
    [SerializeField]
    private bool onStart = true;

    private async void Start()
    {
        if (!onStart)
            return;

        foreach (var sprite in sprites)
        {
            if (this == null || gameObject == null)
                return;

            sRenderer.sprite = sprite;
            await UniTask.Delay(60);
        }
    }

    public async void Animate()
    {
        foreach (var sprite in sprites)
        {
            if (this == null || gameObject == null)
                return;

            sRenderer.sprite = sprite;
            await UniTask.Delay(60);
        }
    }
}
