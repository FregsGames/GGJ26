using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Audios;

public class AudioController : MySerializedSingleton<AudioController>
{
    [SerializeField]
    private AudioSource musicAudioSource;
    [SerializeField]
    private AudioSource effectsAudioSource;
    [SerializeField]
    private Audios audios;

    public void SetSFXVolume(float value)
    {
        effectsAudioSource.volume = value;
    }

    public void SetMusicVolume(float value)
    {
        musicAudioSource.volume = value * .16f;
    }

    public void Play(Clip c)
    {
        var audioclip = audios.Get(c);

        if (audioclip != null)
        {
            effectsAudioSource.pitch = Random.Range(0.8f, 1.2f);
            effectsAudioSource.PlayOneShot(audioclip);
        }
        else
        {
            Debug.LogWarning($"Trying to play clip that has no audioclip ({c.ToString()})");
        }
    }

    public void PlayRandom(List<Clip> cs)
    {
        var c = cs[Random.Range(0, cs.Count - 1)];
        var audioclip = audios.Get(c);

        if (audioclip != null)
        {
            effectsAudioSource.pitch = Random.Range(.90f, 1.1f);
            effectsAudioSource.PlayOneShot(audioclip);
        }
        else
        {
            Debug.LogWarning($"Trying to play clip that has no audioclip ({c.ToString()})");
        }
    }

    public void Play(Music c)
    {
        var audioclip = audios.Get(c);

        if (audioclip != null)
        {
            musicAudioSource.Stop();
            musicAudioSource.clip = audioclip;
            musicAudioSource.Play();
        }
        else
        {
            Debug.LogWarning($"Trying to play clip that has no audioclip ({c.ToString()})");
        }
    }

    public void StopMusic()
    {
        musicAudioSource.Stop();
    }
}
