//using UnityEngine;

public interface IInteractable
{
    void Interact();

    string GetInteractionPrompt();

    bool CanInteractable();
}
