using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
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

    public List<string> shinies;

    public string Current { get => masks[index].id; }
    public List<Sprite> CurrentSprites { get => shinies.Contains(masks[index].id) ? masks[index].shinySprites : masks[index].sprites; }

    public UniTask Prepare()
    {
        index = 0;
        maskImage.sprite = masks[index].sprites[0];
        maskTmp.text = masks[index].id.Localize();
        shinies = new List<string>() { };

        return UniTask.CompletedTask;
    }

    public bool HasShiny(string id)
    {
        return shinies.Contains(id);
    }

    public void ObtainShiny(string k)
    {
        shinies.Add(k);
        characterAnimation.UpdateMask();
    }

    public UniTask Setup()
    {
        return UniTask.CompletedTask;
    }

    public void Swap(bool up)
    {
        index = up ? index + 1 : index - 1;

        if (index < 0)
        {
            index = masks.Count - 1;
        }

        if (index > masks.Count - 1)
        {
            index = 0;
        }

        if (shinies.Contains(masks[index].id))
        {
            maskImage.sprite = masks[index].shinySprites[0];
        }
        else
        { 
            maskImage.sprite = masks[index].sprites[0];
        }

        maskTmp.text = masks[index].id.Localize();

        characterAnimation.UpdateMask();

    }
}

[Serializable]
public class MaskData
{
    public List<Sprite> sprites;
    public List<Sprite> shinySprites;
    public string id;
}