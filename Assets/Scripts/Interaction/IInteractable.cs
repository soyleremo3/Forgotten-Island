using UnityEngine;

namespace SandBoxGame.Interaction
{
    public interface IInteractable
    {
        void Interact(GameObject interactor);

        string GetInteractionPrompt();

        bool CanInteract {  get; }
    }
}