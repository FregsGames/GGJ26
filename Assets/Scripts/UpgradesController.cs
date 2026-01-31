using Cysharp.Threading.Tasks;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradesController : MonoBehaviour, IController
{
    [SerializeField]
    private UpgradeElement upgradeElementPrefab;
    [SerializeField]
    private Transform upgradesContainer;
    [SerializeField]
    private List<UpgradeData> upgrades;

    private Dictionary<string, int> currentUpgrades;

    public int Level(string upgrade)
    {
        return currentUpgrades[upgrade];
    }

    public void ResolveLevelUp()
    {
        var u = upgrades.Where(u => currentUpgrades[u.id] < u.amount).OrderBy(c => Random.value).Take(3).ToList();

        if(u.Count > 0)
        {
            MyUtils.DestroyChildren(upgradesContainer);
            Time.timeScale = .0f;
            upgradesContainer.gameObject.SetActive(true);
            BlockController.Instance.Block(upgradesContainer);

            foreach(var upgrade in u)
            {
                var p = Instantiate(upgradeElementPrefab, upgradesContainer);
                p.Setup(this, upgrade, currentUpgrades[upgrade.id]);
            }
        }
    }

    public UniTask Prepare()
    {
        currentUpgrades = new Dictionary<string, int>();

        foreach (var upgrade in upgrades)
        {
            currentUpgrades.Add(upgrade.id, 0);
        }

        return UniTask.CompletedTask;
    }

    public UniTask Setup()
    {
        return UniTask.CompletedTask;
    }

    public void ResolveUpgrade(string id)
    {
        if(currentUpgrades.ContainsKey(id))
        {
            currentUpgrades[id]++;
        }
        else
        {
            currentUpgrades.Add(id, 1);
        }
        upgradesContainer.gameObject.SetActive(false);
        Time.timeScale = 1f;
        BlockController.Instance.Unblock(upgradesContainer);
    }
}
