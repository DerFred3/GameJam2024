using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float cameraMovementMultiplier = 0.01f;

    [Header("Scroll Settings")]
    [SerializeField] float cameraScrollMultiplier = 0.1f;
    [SerializeField] int cameraScrollMinimum;
    [SerializeField] int cameraScrollMaximum;

    private Camera cam;
    private bool rightMouseDown = false;
    private Vector3 mousePosition;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            rightMouseDown = true;
            mousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(1))
        {
            rightMouseDown = false;
        }

        if (rightMouseDown)
        {
            cam.transform.position += (mousePosition - Input.mousePosition) * cameraMovementMultiplier;
            mousePosition = Input.mousePosition;
        }

        if (Input.mouseScrollDelta.y != 0)
        {
            cam.orthographicSize += -Input.mouseScrollDelta.y * cameraScrollMultiplier;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, cameraScrollMinimum, cameraScrollMaximum);
        }
        
    }
}
