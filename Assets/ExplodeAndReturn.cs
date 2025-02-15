using UnityEngine;
using UnityEngine.UI;

public class ExplodeAndReturn : MonoBehaviour
{
    public float explosionForce = 50f;
    public float returnTime = 10f;
    private Vector3[] originalPositions;
    private bool exploded = false;

    public GameObject infoPanelPrefab; // Prefab for the info panel
    public static GameObject currentInfoPanel; // Reference to the currently active info panel (shared)

    void Start()
    {
        // Store original positions of the parts
        int childCount = transform.childCount;
        originalPositions = new Vector3[childCount];
        for (int i = 0; i < childCount; i++)
        {
            originalPositions[i] = transform.GetChild(i).localPosition;

            // Add Rigidbody for interaction if not already present
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
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform part = transform.GetChild(i);
                MeshRenderer mesh = part.GetComponent<MeshRenderer>();

                // Calculate new position for explosion effect
                Vector3 pos = mesh.bounds.center - transform.position;
                Vector3 newPos = 5 * pos;
                part.position = newPos;
            }

            Invoke("ReturnParts", returnTime);
        }
    }

    //private void ReturnParts()
    //{
    //    for (int i = 0; i < transform.childCount; i++)
    //    {
    //        Transform part = transform.GetChild(i);
    //        part.localPosition = Vector3.Lerp(part.localPosition, originalPositions[i], Time.deltaTime * 2);
    //    }

    //    exploded = false;
    //}

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Explode();
        }
    }
}
