using SandBoxGame.Interaction;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float interactionDistance = 4f;
    [SerializeField] private LayerMask interactableLayer;

    private Camera mainCamera;
    private IInteractable currentInteractable;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        HandleDetection();
        HandleInteractionInput();
    }

    private void HandleDetection()
    {
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        // Explicitly request that the raycast hits trigger colliders too.
        // Chests and NPCs use a trigger collider as their proximity/detection
        // zone, so we can't rely on the project's default physics setting here.
        bool didHit = Physics.Raycast(
            ray,
            out RaycastHit hit,
            interactionDistance,
            interactableLayer,
            QueryTriggerInteraction.Collide);

        if (didHit)
        {
            // GetComponentInParent instead of TryGetComponent: the trigger
            // collider that detects the player is often a separate (larger)
            // child object rather than the exact object the script lives on.
            IInteractable interactable = hit.collider.GetComponentInParent<IInteractable>();

            if (interactable != null)
            {
                currentInteractable = interactable;
                UIManager.Instance.SetInteractionText(currentInteractable.GetInteractionPrompt());
                return;
            }
        }

        ClearCurrentInteractable();
    }

    private void HandleInteractionInput()
    {
        if (currentInteractable == null) return;
        if (!currentInteractable.CanInteract) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            currentInteractable.Interact(gameObject);
        }
    }

    private void ClearCurrentInteractable()
    {
        if (currentInteractable == null) return;

        currentInteractable = null;
        UIManager.Instance.ClearInteractionText();
        UIManager.Instance.ClearDialogueText();
    }
}










/*using SandBoxGame.Interaction;
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
*/