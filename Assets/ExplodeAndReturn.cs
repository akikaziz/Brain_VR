using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class ExplodeAndReturn : MonoBehaviour
{
    private VRInputActions inputActions;

    public Transform[] brainPcs;
    public Quaternion[] origRot;

    public GameObject infoPanelPrefab;     // Prefab for the info panel
    public GameObject functionPanelPrefab; // Prefab for the function panel

    private Vector3[] originalPositions;
    private bool exploded = false;

    // Default instructional content
    private readonly string defaultInfoText = 
        "Welcome to the interactive 3D brain exploration experience!\n\n" +
        "Please follow the instructions below and on the other whiteboard to navigate and interact with the virtual brain components using your VR controllers:\n\n" +
        "You are in a free-roaming 3D space. Use the following controls to move and explore the virtual environment efficiently:\n\n" +
        "Locomotion -\n\n" +
        "• Use the joystick on the left-hand controller to move in the direction you're facing.\n\n" +
        "• To ascend (fly upward), look upward and push the left joystick forward. This allows for vertical movement in addition to horizontal navigation.\n\n" +
        "Rotation-\n\n" +
        "• Use the joystick on the right-hand controller to rotate your view. This enables you to turn left or right without physically rotating your body.";

    private readonly string defaultFunctionText =
        "Please follow the instructions below to navigate and interact with the virtual brain components using your VR controllers:\n\n" +
        "Exploded View Navigation -\n\n" +
        "• Press the A button on your right-hand controller to enter Exploded View mode. This will spatially separate the brain into its 141 anatomical components, allowing you to explore the structure in detail.\n\n" +
        "• Press the B button to return to the original, intact brain configuration.\n\n" +
        "Component Interaction -\n\n" +
        "• Hold the Grab button (grip button) on either controller to select and hold an individual brain component.\n\n" +
        "• While holding a component, press the Trigger button to display detailed information about the selected brain part.\n\n" +
        "• Use the joystick on the same controller to move the component closer/further or rotate it for closer inspection, while still holding the component.";

    void Start()
    {
        int childCount = transform.childCount;

        brainPcs = new Transform[childCount];
        originalPositions = new Vector3[childCount];
        origRot = new Quaternion[childCount];

        for (int i = 0; i < childCount; i++)
        {
            brainPcs[i] = transform.GetChild(i);
            origRot[i] = brainPcs[i].rotation;
            originalPositions[i] = brainPcs[i].position;

            Transform child = transform.GetChild(i);
            if (child.GetComponent<Rigidbody>() == null)
                child.gameObject.AddComponent<Rigidbody>();
        }
    }

    public void Explode()
    {
        if (!exploded)
        {
            exploded = true;
            for (int i = 0; i < brainPcs.Length; i++)
            {
                Transform part = brainPcs[i];

                MeshRenderer mesh = part.GetComponent<MeshRenderer>();
                Vector3 pos = mesh.bounds.center;
                Debug.Log("mesh.bound.center = " + mesh.bounds.center);
                Debug.Log("transform.pos = " + transform.position);
                Vector3 newPos = 5 * pos;
                part.position = newPos;
            }
        }
    }

    public void Return()
    {
        exploded = false;

        for (int i = 0; i < brainPcs.Length; i++)
        {
            Transform part = brainPcs[i];
            Rigidbody rb = part.GetComponent<Rigidbody>();
            part.position = originalPositions[i];
            part.rotation = origRot[i];
            part.SetParent(transform);

            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }

        // Restore instructional content to info panel
        TMP_Text infoText = infoPanelPrefab.GetComponentInChildren<TMP_Text>();
        if (infoText != null)
        {
            infoText.text = defaultInfoText;
            infoPanelPrefab.SetActive(true);
        }

        // Restore instructional content to function panel
        TMP_Text functionText = functionPanelPrefab.GetComponentInChildren<TMP_Text>();
        if (functionText != null)
        {
            functionText.text = defaultFunctionText;
            functionPanelPrefab.SetActive(true);
        }
    }

    private void Awake()
    {
        inputActions = new VRInputActions();
    }

    private void OnEnable()
    {
        inputActions.VRControls.ExplodeAction.performed += ctx => Explode();
        inputActions.VRControls.ReturnAction.performed += ctx => Return();
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}
