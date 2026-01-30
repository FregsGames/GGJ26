using Assets.SimpleLocalization.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MySerializedSingleton<GameManager>
{
    [SerializeField]
    private Camera uiCamera;
    [SerializeField]
    private List<IController> controller;

    public Camera Cam { get => uiCamera; }
    public bool Loaded { get; private set; }

    private async void Start()
    {
        LocalizationManager.Language = "Spanish";
        LocalizationManager.Read();

        foreach (var controller in controller)
        {
            await controller.Prepare();
        }

        foreach (var controller in controller)
        {
            await controller.Setup();
        }

        AudioController.Instance.Play(Audios.Music.BaseSong);
        Loaded = true;
    }
}
