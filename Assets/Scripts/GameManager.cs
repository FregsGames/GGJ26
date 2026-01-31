using Assets.SimpleLocalization.Scripts;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MySerializedSingleton<GameManager>
{
    [SerializeField]
    private Camera uiCamera;
    [SerializeField]
    private List<IController> controller;
    [SerializeField]
    private List<ITickeable> tickeables;
    [SerializeField]
    private List<IFixedTickeable> fixedTickeables;

    public Camera Cam { get => uiCamera; }
    public bool Loaded { get; private set; }

    [Button]
    public void AssingControllers()
    {
        controller = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.InstanceID).OfType<IController>().ToList();
        tickeables = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.InstanceID).OfType<ITickeable>().ToList();
        fixedTickeables = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.InstanceID).OfType<IFixedTickeable>().ToList();
    }

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

        FadeController.Instance.InstantFade();
        _=FadeController.Instance.Unfade();
    }

    private void Update()
    {
        if (Loaded)
        {
            tickeables.ForEach(t => t.Tick());
        }
    }

    private void FixedUpdate()
    {
        if (Loaded)
        {
            fixedTickeables.ForEach(t => t.FixedTick());
        }
    }
}
