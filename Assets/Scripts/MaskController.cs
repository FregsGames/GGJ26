using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MaskController : MonoBehaviour, IController
{
    [SerializeField]
    private List<MaskData> masks;
    [SerializeField]
    private Image maskImage;
    [SerializeField]
    private TextMeshProUGUI maskTmp;

    private int index;

    public UniTask Prepare()
    {
        index = 0;
        maskImage.color = masks[index].color;
        maskTmp.text = masks[index].id.Localize();

        return UniTask.CompletedTask;
    }

    public UniTask Setup()
    {
        return UniTask.CompletedTask;
    }

    public void Swap(bool up)
    {
        index = up ? index + 1: index - 1;

        if(index < 0)
        {
            index = masks.Count - 1;
        }

        if(index > masks.Count - 1)
        {
            index = 0;
        }

        maskImage.color = masks[index].color;
        maskTmp.text = masks[index].id.Localize();

    }
}

[Serializable]
public class MaskData
{
    public Color color;
    public string id;
}