using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI interactionText;

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
}
