using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BrainComponentInfo : MonoBehaviour
{
    public GameObject infoPanelPrefab; // Prefab for the info panel

    [Header("Component Info")]
    public string componentName;
    [TextArea]
    public string description;

    private TMP_Text infoText;

    public bool panel_found = false;

    void Start()
    {
        // Find the TextMeshPro component inside the panel
        infoText = infoPanelPrefab.GetComponentInChildren<TMP_Text>(true);
        //if (infoText != null ) panel_found = true;

        // Ensure the panel is initially hidden
        //infoPanelPrefab.SetActive(false);
    }

    public void ShowInfo(ActivateEventArgs activateEventArgs)
    {
        // Ensure the panel is active
        infoPanelPrefab.SetActive(true);

        // Update the text content
        infoText.text = $"{componentName}\n{description}";
    }
    
}
