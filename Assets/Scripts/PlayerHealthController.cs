using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour, IController
{
    [SerializeField]
    private float startingHealth;
    [SerializeField]
    private Image healthBar;
    [SerializeField]
    private float currentHealth;
    [SerializeField]
    private TextMeshProUGUI healthTMP;

    public float StartingHealth { get => startingHealth; }
    public float MaxHealth { get; set; }

    public UniTask Prepare()
    {
        MaxHealth = startingHealth;
        currentHealth = MaxHealth;
        healthBar.fillAmount = 1f;
        healthTMP.text = $"{currentHealth.ToString("F0")}/{MaxHealth.ToString("F0")}";
        return UniTask.CompletedTask;
    }

    public void ReceiveDamage(float damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            GameManager.Instance.ResolveDeath();
        }

        healthBar.fillAmount = currentHealth / MaxHealth;
        healthTMP.text = $"{currentHealth.ToString("F0")}/{MaxHealth.ToString("F0")}";
    }

    public void Heal(float amount)
    {
        currentHealth += amount;

        if (currentHealth > MaxHealth)
        {
            currentHealth = MaxHealth;
        }

        healthBar.fillAmount = currentHealth / MaxHealth;
        healthTMP.text = $"{currentHealth.ToString("F0")}/{MaxHealth.ToString("F0")}";
    }


    public UniTask Setup()
    {
        return UniTask.CompletedTask;
    }
}
