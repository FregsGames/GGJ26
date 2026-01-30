using UnityEngine;

public class Test : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            DialogueController.Instance.ShowDialogue("dialogue", false);
        }
    }
}
