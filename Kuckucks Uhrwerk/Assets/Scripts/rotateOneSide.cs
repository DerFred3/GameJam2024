using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateOneSide : MonoBehaviour
{
    [SerializeField] private Transform platform;
    [SerializeField] private Transform pivot;
    [SerializeField] private CrankHandle globalCrank;
    [SerializeField] private PivotOrientation pivotOrientation;

    float orientationFactor = 1f;
    private BoxCollider2D platformCollider;

    private enum PivotOrientation
    {
        Left,
        Middle,
        Right,
    }

    private void Start()
    {
        platformCollider = platform.GetComponentInChildren<BoxCollider2D>();

        switch (pivotOrientation)
        {
            case PivotOrientation.Left:
                orientationFactor = platformCollider.size.x / 2;
                break;
            case PivotOrientation.Middle:
                orientationFactor = 0f;
                break;
            case PivotOrientation.Right:
                orientationFactor = -(platformCollider.size.x / 2);
                break;
        }
    }

    void Update()
    {
        Vector3 platformRotation = platform.eulerAngles;
        platformRotation.z = -globalCrank.GetCurrentValue(-90f, 90f);
        platform.rotation = Quaternion.Euler(platformRotation);

        Vector3 platformPosition = pivot.position;
        
        platformPosition.x += Mathf.Sin(globalCrank.GetCurrentValue(0, Mathf.PI)) * orientationFactor;
        platformPosition.y += Mathf.Cos(globalCrank.GetCurrentValue(0, Mathf.PI)) * orientationFactor;
        platform.position = platformPosition;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(pivot.position, 0.1f);
    }
}
