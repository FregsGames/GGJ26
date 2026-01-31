using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpController : MonoBehaviour, IController
{
    [SerializeField]
    private Image experienceBar;
    [SerializeField]
    private float requiredBaseExp;
    [SerializeField]
    private TextMeshProUGUI levelTMP;
    [SerializeField]
    private UpgradesController upgradesController;

    private int level = 1;

    private float currentRequiredExp;
    private float currentExp;

    public int Level { get => level; }

    public void Gain(float exp)
    {
        currentExp += exp;

        if(currentExp > currentRequiredExp)
        {
            var left = currentExp - currentRequiredExp;
            LevelUp();
            currentExp = 0;
            Gain(left);
        }
        else
        {
            experienceBar.fillAmount = currentExp / currentRequiredExp;
        }
    }

    public void LevelUp()
    {
        level++;
        currentRequiredExp = currentRequiredExp * 1.5f;
        levelTMP.text = "level".Localize() + " " + level.ToString();
        upgradesController.ResolveLevelUp();
    }

    public UniTask Prepare()
    {
        currentRequiredExp = requiredBaseExp;
        return UniTask.CompletedTask;
    }

    public UniTask Setup()
    {
        return UniTask.CompletedTask;
    }
}
