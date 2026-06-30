using SandBoxGame.Interaction;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float interactionDistance = 20f;
    [SerializeField] private LayerMask interactableLayer;

    private Camera mainCamera;
    private IInteractable currentInteractable;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
        {
            if (hit.collider.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                currentInteractable = interactable;

                UIManager.Instance.SetInteractionText(currentInteractable.GetInteractionPrompt());
            }
            else
            {
                currentInteractable = null;
                UIManager.Instance.ClearInteractionText();
            }
        }
        else
        {
            if (currentInteractable != null)
            {
                currentInteractable = null;
                UIManager.Instance.ClearInteractionText();
            }
        }

        if (currentInteractable != null && Input.GetKeyDown(KeyCode.E))
        {
            currentInteractable.Interact(gameObject);
        }
    }
}
