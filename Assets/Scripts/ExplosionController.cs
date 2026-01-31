using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExplosionController : MonoBehaviour, IController
{
    [SerializeField]
    private int poolSize;
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private float radius;

    private List<GameObject> pool;

    public UniTask Prepare()
    {
        pool = new List<GameObject>();

        for (int i = 0; i< poolSize; i++)
        {
            var e = Instantiate(prefab);
            pool.Add(e);
            e.SetActive(false);
        }

        return UniTask.CompletedTask;
    }

    public async void Explosion(Vector3 pos, int amount)
    {
        var tempList = new List<GameObject>();

        if(pool.Count < amount)
        {
            for (int i = 0; i < amount; i++)
            {
                var e = Instantiate(prefab);
                tempList.Add(e);
                e.SetActive(false);
            }
        }
        else
        {
            tempList = pool.Take(amount).ToList();
            foreach (var e in tempList)
            {
                pool.Remove(e);
            }
        }

        foreach (var e in tempList)
        {
            e.transform.position = pos;
            e.SetActive(true);
            var size = .35f +  Random.value * .25f;
            e.transform.localScale = new Vector3(size, size, 1);
            var r = Quaternion.Euler(Vector3.forward * Random.value * 360);
            e.transform.rotation = r;
            e.transform.DOMove(pos + new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius)), 0.3f).SetEase(Ease.OutCubic);
            e.transform.DOScale(0, 0.3f);
        }

        await UniTask.Delay(310);

        foreach (var e in tempList)
        {
            e.SetActive(false);
            pool.Add(e);
        }
    }

    public UniTask Setup()
    {
        return UniTask.CompletedTask;
    }
}
