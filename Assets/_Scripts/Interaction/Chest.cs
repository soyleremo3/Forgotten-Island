using SandBoxGame.Interaction;
using UnityEngine;

public enum ChestState
{
    Closed,
    Opened
}

public enum ChestContentType
{
    Reward,
    Trap,
    Empty
}

public class Chest : MonoBehaviour, IInteractable
{
    [Header("References")]
    [SerializeField] private Animator chestAnimator;

    [Header("Content")]
    [SerializeField] private ChestContentType contentType = ChestContentType.Reward;

    [Header("Feedback Timing")]
    [Tooltip("How long the result message (e.g. 'You are Lucky') stays on screen after opening, before falling back to 'Chest is Empty'.")]
    [SerializeField] private float resultMessageDuration = 3f;

    // Cached hash for the Animator trigger parameter — avoids string comparisons every call.
    private static readonly int OpenTrigger = Animator.StringToHash("Open");

    private ChestState currentState = ChestState.Closed;
    private float resultMessageEndTime;

    public bool CanInteract => currentState == ChestState.Closed;

    public string GetInteractionPrompt()
    {
        if (currentState == ChestState.Closed)
        {
            return "Press 'E' to Open the Chest";
        }

        // Just opened: show the result message until it expires, then permanently
        // fall back to the "empty" status. PlayerInteractor reads this every frame,
        // so the transition from result message -> empty message is automatic —
        // no coroutine or extra update loop needed.
        if (Time.time < resultMessageEndTime)
        {
            return GetResultMessage();
        }

        return "Chest is Empty";
    }

    public void Interact(GameObject interactor)
    {
        if (currentState != ChestState.Closed) return;

        currentState = ChestState.Opened;
        resultMessageEndTime = Time.time + resultMessageDuration;

        chestAnimator.SetTrigger(OpenTrigger);

        // Refresh immediately so the result message appears on the same frame
        // the chest opens, instead of waiting for the next Update tick.
        UIManager.Instance.SetInteractionText(GetInteractionPrompt());
    }

    private string GetResultMessage()
    {
        return contentType switch
        {
            ChestContentType.Reward => "You are Lucky!",
            ChestContentType.Trap => "You Dead",
            ChestContentType.Empty => "Chest is Empty",
            _ => "Chest is Empty"
        };
    }
}

















/*using SandBoxGame.Interaction;
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
        };*/


/*if (currentState == ChestState.opened)
{
    UIManager.Instance.SetInteractionText("The Chest Opened Look acquired");
}

return "Press 'E' to Open Chest";*//*
}

public void Interact(GameObject interactor)
{
if (currentState != ChestState.closed) return;

currentState = ChestState.opened;

chestAnimator.SetTrigger("Open");

//UIManager.Instance.SetInteractionText(GetInteractionPrompt());
}
}
*/