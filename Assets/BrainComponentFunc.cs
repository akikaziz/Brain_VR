using TMPro;
using UnityEngine;

public class BrainComponentFunc : MonoBehaviour
{
    public GameObject functionPanelPrefab; // Prefab for the info panel

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
        functionPanelPrefab.SetActive(false);
    }

    public void ShowInfo()
    {
        // Ensure the panel is active
        functionPanelPrefab.SetActive(true);

        // Update the text content
        funcText.text = $"{functions}\n{otherComps}";
    }

    //void Update()
    //{
    //    if (Input.GetButtonDown("Fire2")) // Detect B button press
    //    {
    //        ShowInfo(); // Update text each time B is pressed
    //    }
    //}
}
