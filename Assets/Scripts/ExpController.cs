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
        currentRequiredExp = currentRequiredExp * 1.05f;
        levelTMP.text = "level".Localize() + " " + level.ToString();
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
