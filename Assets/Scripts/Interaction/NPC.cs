using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    public string GetInteractionPrompt()
    {
        return "Talk";
    }

    public void Interact()
    {
        Debug.Log("NPC: Hello adventurer, I have a quest for you!");
    }
}
