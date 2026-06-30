using SandBoxGame.Interaction;
using UnityEngine;

public enum ChestState
{
    closed,  
    opened
}

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator chestAnimator;

    private ChestState currentState = ChestState.closed;

    public bool CanInteract => currentState == ChestState.closed;

    public string GetInteractionPrompt()
    {
        return currentState switch
        {
            ChestState.closed => "Press 'E' To Open The Chest",
            ChestState.opened => "Empty", 
            _ => ""
        };


        /*if (currentState == ChestState.opened)
        {
            UIManager.Instance.SetInteractionText("The Chest Opened Look acquired");
        }

        return "Press 'E' to Open Chest";*/
    }

    public void Interact(GameObject interactor)
    {
        if (currentState != ChestState.closed) return;

        currentState = ChestState.opened;

        chestAnimator.SetTrigger("Open");

        //UIManager.Instance.SetInteractionText(GetInteractionPrompt());
    }
}
