using Cysharp.Threading.Tasks;
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

    public UniTask Prepare()
    {
        currentHealth = startingHealth;
        healthBar.fillAmount = 1f;
        return UniTask.CompletedTask;
    }

    public void ReceiveDamage(float damage)
    {
        currentHealth -= damage;

        if(currentHealth < 0)
        {
            currentHealth = 0;
        }

        healthBar.fillAmount = currentHealth / startingHealth;
    }

    public UniTask Setup()
    {
        return UniTask.CompletedTask;
    }
}
