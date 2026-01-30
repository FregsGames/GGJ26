using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockController : MySerializedSingleton<BlockController>, IController
{
    [SerializeField]
    private GameObject filter;
    [SerializeField]
    private Canvas canvas;

    private Dictionary<Transform, (Transform, int)> block;

    public UniTask Prepare()
    {
        block = new Dictionary<Transform, (Transform, int)>();
        filter.SetActive(false);
        return UniTask.CompletedTask;
    }

    public UniTask Setup()
    {
        return UniTask.CompletedTask;
    }

    public void Block(Transform t)
    {
        if (block.ContainsKey(t))
            return;

        canvas.worldCamera = Camera.main;
        canvas.sortingLayerName = "UI";

        if (block.Count > 0)
        {
            block.Last().Key.SetParent(block.Last().Value.Item1, false);
            block.Last().Key.SetSiblingIndex(block.Last().Value.Item2);
        }

        filter.SetActive(true);
        block.Add(t, (t.parent, t.GetSiblingIndex()));
        t.SetParent(canvas.transform, false);
        t.SetAsLastSibling();
    }

    public void Unblock(Transform t)
    {
        if (!block.ContainsKey(t))
            return;

        t.SetParent(block[t].Item1, false);
        t.SetSiblingIndex(block[t].Item2);
        block.Remove(t);

        if (block.Count > 0)
        {
            block.Last().Key.SetParent(transform, false);
            block.Last().Key.SetAsLastSibling();
        }
        else
        {
            filter.SetActive(false);
        }
    }

}
