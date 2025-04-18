using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BrainComponentFunc : MonoBehaviour
{
    public GameObject functionPanelPrefab; // Prefab for the func panel

    [Header("Functions")]
    [TextArea]
    public string functions;
    [TextArea]
    public string otherComps;

    private TMP_Text funcText;

    void Start()
    {
        // Find the TextMeshPro component inside the panel
        funcText = functionPanelPrefab.GetComponentInChildren<TMP_Text>(true);

        // Ensure the panel is initially hidden
        //functionPanelPrefab.SetActive(false);
    }

    public void ShowFunc(ActivateEventArgs activateEventArgs)
    {

        // Ensure the panel is active
        functionPanelPrefab.SetActive(true);

        // Update the text content
        funcText.text = $"{functions}\n\n\n{otherComps}";
    }

}
