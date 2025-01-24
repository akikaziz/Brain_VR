using TMPro;
using UnityEngine;

public class BrainComponentInfo : MonoBehaviour
{
    public GameObject infoPanelPrefab; // Prefab for the info panel

    [Header("Component Info")]
    public string componentName;
    [TextArea]
    public string description;

    private void OnMouseDown()
    {
        Debug.Log($"Clicked on {componentName}");

        // Destroy the previous info panel if it exists
        if (ExplodeAndReturn.currentInfoPanel != null)
        {
            Destroy(ExplodeAndReturn.currentInfoPanel);
        }

        // Create a new info panel
        ExplodeAndReturn.currentInfoPanel = Instantiate(infoPanelPrefab);

        // Set the text of the info panel
        TMP_Text infoText = ExplodeAndReturn.currentInfoPanel.GetComponentInChildren<TMP_Text>();
        infoText.text = $"{componentName}\n{description}";

        // Position the panel near the clicked component in world space
        //Vector3 panelPosition = transform.position + new Vector3(0, 0.15f, 0); // Offset the panel above the component
        //ExplodeAndReturn.currentInfoPanel.transform.position = panelPosition;

        // Make the panel face the camera
        ExplodeAndReturn.currentInfoPanel.transform.LookAt(Camera.main.transform);
        //ExplodeAndReturn.currentInfoPanel.transform.Rotate(0, 180, 0); // Adjust orientation
    }
}
