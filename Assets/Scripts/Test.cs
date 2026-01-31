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
    }
}
