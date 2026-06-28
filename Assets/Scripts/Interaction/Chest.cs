using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    public string GetInteractionPrompt()
    {
        return "Open The Chest";
    }

    public void Interact()
    {
        Debug.Log("The Chest Opened Look acquired");
    }
}
