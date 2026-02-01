using Assets.SimpleLocalization.Scripts;
using Cysharp.Threading.Tasks;
using Febucci.TextAnimatorForUnity;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueController : MySerializedSingleton<DialogueController>
{
    [SerializeField]
    private RectTransform container;
    [SerializeField]
    private RectTransform background;
    [SerializeField]
    private TypewriterComponent typewriter;
    [SerializeField]
    private TextMeshProUGUI tmp;
    [SerializeField]
    private RectTransform tmpRect;
    [SerializeField]
    private RectTransform topDialoguePos;
    [SerializeField]
    private RectTransform bottomDialoguePos;

    public static Action OnConversationFlagAdded;

    public bool OnDialogue { get; private set; }

    public void Start()
    {
        container.gameObject.SetActive(false);
    }


    public async UniTask ShowDialogue(string conversationID, bool top)
    {
        PositionContainer(top);
        await ResolveDialogue(conversationID);
    }

    public async UniTask ShowDialogueWithReplace(RectTransform rect, string conversationID, string replace)
    {
        PositionContainer(rect);
        await ResolveDialogue(conversationID, replace);
    }

    private async UniTask ResolveDialogue(string conversationID, string replace = "")
    {
        OnDialogue = true;

        tmp.text = "";
        container.gameObject.SetActive(true);
        BlockController.Instance.Block(container);

        var textIDs = GetConversationIDs(conversationID);

        if (textIDs != null && textIDs.Count > 0)
        {
            foreach (string id in textIDs)
            {
                if (string.IsNullOrEmpty(replace))
                {
                    typewriter.ShowText(id.Localize());
                }
                else
                {
                    typewriter.ShowText(id.Localize().Replace("{0}", replace.Localize()));
                }
                bool canSkip = !Input.anyKey;
                while (typewriter.IsShowingText)
                {
                    if (!canSkip)
                    {
                        if (!Input.anyKey)
                        {
                            canSkip = true;
                        }
                    }

                    if (canSkip && Input.anyKey)
                    {
                        typewriter.SkipTypewriter();
                        break;
                    }

                    await UniTask.Yield();
                }

                await UniTask.WaitUntil(() => !Input.anyKey);
                await UniTask.Delay(300, ignoreTimeScale: true);
                await UniTask.WaitUntil(() => Input.anyKey);

            }
        }

        container.gameObject.SetActive(false);
        BlockController.Instance.Unblock(container);

        OnDialogue = false;
    }

    private List<string> GetConversationIDs(string conversation)
    {
        if (LocalizationManager.HasKey(conversation))
        {
            return new List<string>() { conversation };
        }
        else
        {
            int i = 0;
            List<string> texts = new List<string>();

            while (true)
            {
                if (LocalizationManager.HasKey($"{conversation}_{i}"))
                {
                    texts.Add($"{conversation}_{i}");
                    i++;
                }
                else
                {
                    break;
                }
            }

            if (texts.Count == 0)
            {
                texts.Add(conversation);
            }

            return texts;
        }
    }

    private void PositionContainer(bool topPos)
    {
        container.pivot = new Vector2(0.5f, 0.5f);

        container.position = topPos ? topDialoguePos.position : bottomDialoguePos.position;

        background.localScale = Vector3.one;

        var top = background.localScale.y >= 0 ? 0 : 55;
        var bottom = background.localScale.y >= 0 ? 55 : 0;

        tmpRect.offsetMin = new Vector2(tmpRect.offsetMin.x, bottom);
        tmpRect.offsetMax = new Vector2(tmpRect.offsetMax.x, -top);

    }

    private void PositionContainer(RectTransform rect)
    {
        var pos = GameManager.Instance.Cam.WorldToScreenPoint(rect.position);

        var w = GameManager.Instance.Cam.pixelWidth;
        var h = GameManager.Instance.Cam.pixelHeight;

        bool leftScreen = pos.x <= w / 2f;
        bool bottomScreen = pos.y <= h / 2f;

        container.pivot = new Vector2(leftScreen ? 0 : 1, bottomScreen ? 0 : 1);

        Vector3 offset = new Vector2(leftScreen ? rect.rect.width / 2f : -rect.rect.width / 2f, bottomScreen ? rect.rect.height / 2f : -rect.rect.height / 2f);
        container.transform.position = pos + offset;

        var dir = (container.transform.position - pos).normalized;

        background.localScale = new Vector3(dir.x >= 0 ? -1 : 1, dir.y >= 0 ? 1 : -1, 1);


        var top = background.localScale.y >= 0 ? 0 : 55;
        var bottom = background.localScale.y >= 0 ? 55 : 0;

        tmpRect.offsetMin = new Vector2(tmpRect.offsetMin.x, bottom);
        tmpRect.offsetMax = new Vector2(tmpRect.offsetMax.x, -top);
    }
}
