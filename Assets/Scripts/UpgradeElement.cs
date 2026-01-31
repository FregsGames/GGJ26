using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeElement : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI title;
    [SerializeField]
    private TextMeshProUGUI desc;
    [SerializeField]
    private Image img;
    [SerializeField]
    private Button button;

    private UpgradesController controller;
    private UpgradeData upgradeData;
    public void Setup(UpgradesController controller, UpgradeData data, int level)
    {
        this.upgradeData = data;
        this.controller = controller;
        title.text = data.id.Localize() + $" +{level + 1}";
        desc.text = data.desc.Localize();
        img.sprite = data.sprite;
    }

    private void OnEnable()
    {
        button.onClick.AddListener(ResolveClick);
    }

    private void ResolveClick()
    {
        controller.ResolveUpgrade(upgradeData.id);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(ResolveClick);
    }
}
