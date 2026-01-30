using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MySerializedSingleton<OptionsController>, IController
{
    [SerializeField]
    private Button openOptionsButton;
    [SerializeField]
    private GameObject container;
    [SerializeField]
    private Slider musicVol;
    [SerializeField]
    private Slider effectsVol;
    [SerializeField]
    private Button closeButton;

    private void OnEnable()
    {
        closeButton.onClick.AddListener(ClosePanel);
        musicVol.onValueChanged.AddListener(UpdateMusic);
        effectsVol.onValueChanged.AddListener(UpdateEffect);
        openOptionsButton.onClick.AddListener(OpenPanel);
    }

    private void UpdateMusic(float vol)
    {
        AudioController.Instance.SetMusicVolume(vol);
        PlayerPrefs.SetFloat("musicVol", vol);
    }

    private void UpdateEffect(float vol)
    {
        AudioController.Instance.SetSFXVolume(vol);
        PlayerPrefs.SetFloat("sfxVol", vol);
    }


    private void OpenPanel()
    {
        container.SetActive(true);
        BlockController.Instance.Block(container.transform);
    }

    private void ClosePanel()
    {
        container.SetActive(false);
        BlockController.Instance.Unblock(container.transform);
    }

    private void OnDisable()
    {
        closeButton.onClick.RemoveListener(ClosePanel);
        musicVol.onValueChanged.RemoveListener(UpdateMusic);
        effectsVol.onValueChanged.RemoveListener(UpdateEffect);
        openOptionsButton.onClick.RemoveListener(OpenPanel);
    }

    public UniTask Prepare()
    {
        AudioController.Instance.SetMusicVolume(PlayerPrefs.GetFloat("musicVol", 1));
        AudioController.Instance.SetSFXVolume(PlayerPrefs.GetFloat("sfxVol", 1));

        musicVol.SetValueWithoutNotify(PlayerPrefs.GetFloat("musicVol", 1));
        effectsVol.SetValueWithoutNotify(PlayerPrefs.GetFloat("sfxVol", 1));

        return UniTask.CompletedTask;
    }

    public UniTask Setup()
    {
        return UniTask.CompletedTask;
    }
}
