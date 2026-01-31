using UnityEngine;

public class Test : MonoBehaviour
{
    public CharacterAnimation character;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            DialogueController.Instance.ShowDialogue("dialogue", false);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            ConfirmationController.Instance.AskForConfirmation("Confirm?", "Yes", "NO");
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            FindFirstObjectByType<PlayerHealthController>().ReceiveDamage(100);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            GameManager.Instance.ResolveDeath();
        }
    }
}
