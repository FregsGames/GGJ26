using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour, IController
{
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Button creditsButton;
    [SerializeField]
    private Button quitButton;

    [SerializeField]
    private GameObject menu;

    private void OnEnable()
    {
        playButton.onClick.AddListener(Play);
        creditsButton.onClick.AddListener(ShowCredits);
        quitButton.onClick.AddListener(Quit);
    }

    private async void Play()
    {
        menu.SetActive(false);
        await ConfirmationController.Instance.AskForConfirmation("start".Localize(), "Chachi", "nice".Localize());
        await ConfirmationController.Instance.AskForConfirmation("mask.tuto".Localize(), "ok".Localize(), "nice".Localize());
    }

    private void ShowCredits()
    {
        _=ConfirmationController.Instance.AskForConfirmation("credits.value".Localize(), "ok".Localize(), "nice".Localize());
    }

    private void Quit()
    {
        Application.Quit();
    }

    private void OnDisable()
    {
        playButton.onClick.RemoveListener(Play);
        creditsButton.onClick.RemoveListener(ShowCredits);
        quitButton.onClick.RemoveListener(Quit);
    }

    public UniTask Prepare()
    {
        if (GameManager.Instance.AlreadyStarted)
        {
            menu.SetActive(false);
            return UniTask.CompletedTask;
        }

        menu.SetActive(true);
        Time.timeScale = 0.0f;
        return UniTask.CompletedTask;
    }

    public UniTask Setup()
    {
        return UniTask.CompletedTask;
    }
}
