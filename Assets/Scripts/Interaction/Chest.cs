using UnityEngine;

public enum ChestState
{
    closed, 
    opening, 
    opened
}

public class Chest : MonoBehaviour, IInteractable
{
    private ChestState currentState = ChestState.closed;

    public bool CanInteractable()
    {
        return currentState == ChestState.closed;
    }

    public string GetInteractionPrompt()
    {
        if (currentState == ChestState.opening || currentState == ChestState.opened)
        {
            UIManager.Instance.SetInteractionText("The Chest Opened Look acquired");
        }

        return "Press 'E' to Open Chest";
    }

    public void Interact()
    {
        if (currentState != ChestState.closed) return;

        currentState = ChestState.opening;

        UIManager.Instance.SetInteractionText(GetInteractionPrompt());
    }
}
