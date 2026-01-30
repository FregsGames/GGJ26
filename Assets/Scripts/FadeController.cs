using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MySerializedSingleton<FadeController>, IController
{
    [SerializeField]
    private Image loadScreen;

    public UniTask Setup()
    {
        return UniTask.CompletedTask;
    }

    public void InstantFade()
    {
        loadScreen.enabled = true;
        loadScreen.color = Color.black;
    }

    public async UniTask Fade()
    {
        if (loadScreen.enabled)
            return;

        loadScreen.color = Color.clear;
        loadScreen.enabled = true;
        await loadScreen.DOColor(Color.black, 0.5f).SetUpdate(true).AsyncWaitForCompletion();
    }

    public async UniTask Unfade()
    {
        await loadScreen.DOColor(Color.clear, 0.5f).SetUpdate(true).AsyncWaitForCompletion();
        loadScreen.enabled = false;
    }
    public UniTask Prepare()
    {
        loadScreen.enabled = false;
        return UniTask.CompletedTask;
    }
}