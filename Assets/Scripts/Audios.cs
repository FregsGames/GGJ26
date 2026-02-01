using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Audios", menuName = "Scriptable Objects/Audios")]
public class Audios : SerializedScriptableObject
{
    public enum Music
    {
        BaseSong
    }

    public enum Clip
    {
        Click, Impact, Key, Chest, LevelUp, Die1, Die2, Die3
    }

    [SerializeField]
    private Dictionary<Clip, AudioClip> clips;

    [SerializeField]
    private Dictionary<Music, AudioClip> songs;

    public AudioClip Get(Clip clip)
    {
        if (clips.ContainsKey(clip))
        {
            return clips[clip];
        }

        return null;
    }

    public AudioClip Get(Music clip)
    {
        if (songs.ContainsKey(clip))
        {
            return songs[clip];
        }

        return null;
    }
}
