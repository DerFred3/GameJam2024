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

    private enum PivotOrientation
    {
        Left,
        Middle,
        Right,
    }

    private void Start()
    {
        

        switch (pivotOrientation)
        {
            case PivotOrientation.Left:
                orientationFactor = platform.localScale.x / 2;
                break;
            case PivotOrientation.Middle:
                orientationFactor = 0f;
                break;
            case PivotOrientation.Right:
                orientationFactor = -(platform.localScale.x / 2);
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
}
