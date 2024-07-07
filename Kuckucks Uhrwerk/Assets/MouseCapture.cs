using UnityEngine;

public class MouseCapture : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera cam;
    [SerializeField] private RectTransform leftCrank;
    [SerializeField] private RectTransform rightCrank;

    [Header("Settings")]
    [SerializeField] private Vector2 pivotPointOffset;
    [SerializeField] private float crankMoveMultiplier;
    [SerializeField] private float angleLimit;

    public bool TrackingActive = true;

    private bool leftButton = false;
    private bool rightButton = false;

    private Vector2 mousePos;
    private Vector2 mouseVector;
    private Vector2 pivotPoint;
    private Vector2 pivotPointRelativeToCam;

    private void Update()
    {
        if (!TrackingActive) return;

        // Left-click up
        if (Input.GetMouseButtonUp(0))
        {
            leftButton = false;
        }

        // Right-click up
        if (Input.GetMouseButtonUp(1))
        {
            rightButton = false;
        }

        // Left-click down
        if (Input.GetMouseButtonDown(0))
        {
            leftButton = true;

            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            pivotPoint = mousePos + pivotPointOffset;
            pivotPointRelativeToCam = pivotPoint - (Vector2)cam.transform.position;
            mouseVector = (mousePos - pivotPoint).normalized;
        }

        // Right-click down
        if (Input.GetMouseButtonDown(1))
        {
            rightButton = true;

            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            pivotPoint = mousePos + pivotPointOffset;
            pivotPointRelativeToCam = pivotPoint - (Vector2)cam.transform.position;
            mouseVector = (mousePos - pivotPoint).normalized;
        }

        // Move pivotPoint with cam; keep it relative to cam position
        pivotPoint = (Vector2)cam.transform.position + pivotPointRelativeToCam;

        // Calculate applied rotation
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        float mouseDragAngle = Vector2.SignedAngle(mouseVector, (mousePos - pivotPoint).normalized);
        mouseVector = (mousePos - pivotPoint).normalized;

        if (leftButton)
        {
            // Apply to left crank
            Vector3 rotation = leftCrank.localEulerAngles;
            rotation.z += mouseDragAngle * crankMoveMultiplier;
            rotation.z = Mathf.Clamp(rotation.z, 0f, angleLimit * 2);
            leftCrank.localRotation = Quaternion.Euler(rotation);
        }

        if (rightButton)
        {
            // Apply to right crank
            Vector3 rotation = rightCrank.localEulerAngles;
            rotation.z += mouseDragAngle * crankMoveMultiplier;
            rotation.z = Mathf.Clamp(rotation.z, 0f, angleLimit * 2);
            rightCrank.localRotation = Quaternion.Euler(rotation);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (pivotPoint != null) Gizmos.DrawSphere(pivotPoint, 0.1f);
    }
}
