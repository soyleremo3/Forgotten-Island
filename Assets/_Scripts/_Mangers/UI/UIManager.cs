using TMPro;
using UnityEngine;

/// <summary>
/// NOTE: You already have a UIManager singleton in your project. This file shows
/// the two method pairs (Set/Clear for prompt, Set/Clear for dialogue) that
/// PlayerInteractor, Chest, and NPC expect to exist. Merge this into your
/// existing UIManager rather than replacing it if it already has other logic.
/// </summary>
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Interaction Prompt")]
    [SerializeField] private TextMeshProUGUI interactionPromptText;

    [Header("Dialogue")]
    [SerializeField] private TextMeshProUGUI dialogueText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void SetInteractionText(string text)
    {
        if (interactionPromptText == null) return;
        interactionPromptText.text = text;
    }

    public void ClearInteractionText()
    {
        if (interactionPromptText == null) return;
        interactionPromptText.text = string.Empty;
    }

    public void SetDialogueText(string text)
    {
        if (dialogueText == null) return;
        dialogueText.text = text;
    }

    public void ClearDialogueText()
    {
        if (dialogueText == null) return;
        dialogueText.text = string.Empty;
    }
}
















/*using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI interactionText;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetInteractionText(string message)
    {
        interactionText.text = message;
    }

    public void ClearInteractionText()
    {
        interactionText.text = string.Empty;
    }

    public void ShowDialogue(string message)
    {
        dialogueText.text = message;
    }

    public void ClearDialogue()
    {
        dialogueText.text = string.Empty;
    }
}
*/