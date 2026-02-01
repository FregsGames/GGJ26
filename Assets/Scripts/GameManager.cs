using Assets.SimpleLocalization.Scripts;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField]
    private Texture2D cursor;

    public Camera Cam { get => uiCamera; }
    public bool Loaded { get; private set; }

    public bool AlreadyStarted { get; set; }

    [Button]
    public void AssingControllers()
    {
        controller = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.InstanceID).OfType<IController>().ToList();
        tickeables = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.InstanceID).OfType<ITickeable>().ToList();
        fixedTickeables = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.InstanceID).OfType<IFixedTickeable>().ToList();
    }

    private async void Start()
    {
        Cursor.SetCursor(cursor, new Vector2(cursor.width / 2f, cursor.height / 2f), CursorMode.Auto);
        await UniTask.DelayFrame(10);
        LocalizationManager.Language = "Spanish";
        LocalizationManager.Read();
        await PrepareControllers();

        FadeController.Instance.InstantFade();
        _ = FadeController.Instance.Unfade();

        Time.timeScale = 0.0f;

        AlreadyStarted = true;
    }

    private async UniTask PrepareControllers()
    {
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

    public async void ResolveDeath()
    {
        Loaded = false;
        Time.timeScale = 0.0f;
        bool restart = await ConfirmationController.Instance.AskForConfirmation("death".Localize(), "restart".Localize(), "quit".Localize());
        await FadeController.Instance.Fade();
        if (restart)
        {
            await SceneManager.LoadSceneAsync("Game");
            uiCamera = Camera.main;
            AssingControllers();
            Time.timeScale = 1.0f;

            await PrepareControllers();
            Loaded= true;
            _ = FadeController.Instance.Unfade();
        }
        else
        {
            Application.Quit();
        }
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
