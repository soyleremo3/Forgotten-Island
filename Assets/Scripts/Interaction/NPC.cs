using SandBoxGame.Interaction;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] private string NPCName;
    [SerializeField] private string dialogueText = "Hello adventurer, I have a quest for you!";

    public bool CanInteract => true;

    public string GetInteractionPrompt()
    {
        return "Press 'E' To Talk";
    }

    public void Interact(GameObject interactor)
    {
        UIManager.Instance.SetInteractionText($"{NPCName}: {dialogueText}");
    }
}
