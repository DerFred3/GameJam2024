using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalEntry : MonoBehaviour
{
    [SerializeField] private Transform exitPortal;

    private GameObject trackedObject;
    private Vector2 positionEntered;
    private GameObject ghostInstance;
    private Vector2 trackedObjectGhostOffset;

    private void Awake()
    {
        if (exitPortal == null)
        {
            Debug.LogError("Help I am a portal, but I have no exit portal :(");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player") return;

        trackedObject = other.transform.root.gameObject;
        positionEntered = trackedObject.transform.position;

        Vector2 differenceTrackedPortal = transform.position - trackedObject.transform.position;

        ghostInstance = Instantiate(trackedObject, 
            exitPortal.transform.position - (Vector3)differenceTrackedPortal, 
            trackedObject.transform.rotation);

        trackedObjectGhostOffset = trackedObject.transform.position - ghostInstance.transform.position;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag != "Player") return;
        trackedObject.transform.position = ghostInstance.transform.position;

        trackedObject = null;
        Destroy(ghostInstance);
    }

    private void Update()
    {
        if (trackedObject == null) return;

        // Sync position of ghost to position of trackedObject
        ghostInstance.transform.position = (Vector2)trackedObject.transform.position - trackedObjectGhostOffset;
    }
}
