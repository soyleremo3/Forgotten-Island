using SandBoxGame.Interaction;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [Header("References")]
    [SerializeField] private Animator npcAnimator;

    [Header("Dialogue")]
    [SerializeField] private string npcName = "Villager";
    [SerializeField] private string dialogueText = "Hello adventurer, I have a quest for you!";

    private static readonly int TalkTrigger = Animator.StringToHash("Talk");

    // NPCs are always interactable — talking never "runs out" like a chest does.
    public bool CanInteract => true;

    public string GetInteractionPrompt()
    {
        return $"Press 'E' to Talk with {npcName}";
    }

    public void Interact(GameObject interactor)
    {
        if (npcAnimator != null)
        {
            npcAnimator.SetTrigger(TalkTrigger);
        }

        UIManager.Instance.SetDialogueText($"{npcName}: {dialogueText}");
    }
}








/*using SandBoxGame.Interaction;
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
*/