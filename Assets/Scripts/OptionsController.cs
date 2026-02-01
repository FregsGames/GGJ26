using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MySerializedSingleton<OptionsController>
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
    private Toggle shake;
    [SerializeField]
    private Button closeButton;

    public bool Shake { get; set; }

    private void OnEnable()
    {
        closeButton.onClick.AddListener(ClosePanel);
        musicVol.onValueChanged.AddListener(UpdateMusic);
        effectsVol.onValueChanged.AddListener(UpdateEffect);
        openOptionsButton.onClick.AddListener(OpenPanel);
        shake.onValueChanged.AddListener(ToggleShake);
    }

    private void ToggleShake(bool active)
    {
        PlayerPrefs.SetInt("shake", active? 1 : 0);
        Shake = active;
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
        Time.timeScale = 0.0f;
        container.SetActive(true);
        BlockController.Instance.Block(container.transform);
    }

    private void ClosePanel()
    {
        Time.timeScale = 1.0f;
        container.SetActive(false);
        BlockController.Instance.Unblock(container.transform);
    }

    private void OnDisable()
    {
        closeButton.onClick.RemoveListener(ClosePanel);
        musicVol.onValueChanged.RemoveListener(UpdateMusic);
        effectsVol.onValueChanged.RemoveListener(UpdateEffect);
        openOptionsButton.onClick.RemoveListener(OpenPanel);
        shake.onValueChanged.RemoveListener(ToggleShake);
    }

    public void Start()
    {
        AudioController.Instance.SetMusicVolume(PlayerPrefs.GetFloat("musicVol", 1));
        AudioController.Instance.SetSFXVolume(PlayerPrefs.GetFloat("sfxVol", 1));

        musicVol.SetValueWithoutNotify(PlayerPrefs.GetFloat("musicVol", 1));
        effectsVol.SetValueWithoutNotify(PlayerPrefs.GetFloat("sfxVol", 1));

        shake.SetIsOnWithoutNotify(PlayerPrefs.GetInt("shake", 1) == 1);
        Shake = PlayerPrefs.GetInt("shake", 1) == 1;
    }
}
