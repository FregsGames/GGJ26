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
    [SerializeField]
    private CharacterAnimation characterAnimation;

    private int index;

    public string Current { get => masks[index].id; }
    public List<Sprite> CurrentSprites { get => masks[index].sprites; }

    public UniTask Prepare()
    {
        index = 0;
        maskImage.sprite = masks[index].sprites[0];
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

        maskImage.sprite = masks[index].sprites[0];
        maskTmp.text = masks[index].id.Localize();

        characterAnimation.UpdateMask();

    }
}

[Serializable]
public class MaskData
{
    public List<Sprite> sprites;
    public string id;
}