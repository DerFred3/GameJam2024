using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalEntry : MonoBehaviour
{
    [SerializeField] private Transform exitPortal;

    private enum EnterDirection
    {
        Left,
        Right,
    }

    private GameObject trackedObject;
    private BoxCollider2D trackedCollider;
    private Vector2 positionEntered;
    private EnterDirection enterDirection;
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
        trackedObject = other.gameObject;
        trackedCollider = (BoxCollider2D)other;
        positionEntered = trackedObject.transform.position;

        Vector2 differenceTrackedPortal = transform.position - trackedObject.transform.position;

        ghostInstance = Instantiate(trackedObject, 
            exitPortal.transform.position - (Vector3)differenceTrackedPortal, 
            trackedObject.transform.rotation);

        trackedObjectGhostOffset = trackedObject.transform.position - ghostInstance.transform.position;

        if (positionEntered.x < transform.position.x)
        {
            enterDirection = EnterDirection.Left;
        } 
        else
        {
            enterDirection = EnterDirection.Right;
        }
    }

    private void OnTriggerExit2D()
    {
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
