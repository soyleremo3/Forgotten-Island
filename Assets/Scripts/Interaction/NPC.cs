using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    public bool CanInteractable()
    {
        throw new System.NotImplementedException();
    }

    public string GetInteractionPrompt()
    {
        return "Click 'E' For Talk";
    }

    public void Interact()
    {
        UIManager.Instance.SetInteractionText("NPC: Hello adventurer, I have a quest for you!");
    }
}
