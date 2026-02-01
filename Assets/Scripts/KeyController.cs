using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour, IController
{
    private List<string> keys;

    public void ObtainKey(string k)
    {
        keys.Add(k);
    }

    public bool HasKey(string k)
    {
        return keys.Contains(k);
    }

    public UniTask Prepare()
    {
        keys = new List<string>();
        return UniTask.CompletedTask;
    }

    public UniTask Setup()
    {
        return UniTask.CompletedTask;
    }
}
