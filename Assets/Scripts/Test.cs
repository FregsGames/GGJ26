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

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            character.ChangeAnimation(CharacterAnimation.Animations.Idle);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            character.ChangeAnimation(CharacterAnimation.Animations.Run);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            ConfirmationController.Instance.AskForConfirmation("Confirm?", "Yes", "NO");
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
        }
    }
}
