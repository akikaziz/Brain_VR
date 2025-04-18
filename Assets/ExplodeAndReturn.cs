using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class ExplodeAndReturn : MonoBehaviour
{

    private VRInputActions inputActions;

    public Transform[] brainPcs;

    public Quaternion[] origRot;

    public GameObject infoPanelPrefab; // Prefab for the info panel
    public GameObject functionPanelPrefab; // Prefab for the func panel

    private Vector3[] originalPositions;
    private bool exploded = false;

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
                Vector3 pos = mesh.bounds.center - transform.position;
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
        infoPanelPrefab.SetActive(false);
        functionPanelPrefab.SetActive(false);
        
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
