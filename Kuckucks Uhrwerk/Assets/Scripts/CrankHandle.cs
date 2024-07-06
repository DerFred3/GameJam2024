using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrankHandle : MonoBehaviour
{
    [SerializeField] private RectTransform crank;
    [SerializeField] private float crankMoveMultiplier;

    [SerializeField] private float angleLimit;

    private Vector3 mousePosition;

    private bool clickable = true;

    private void Start()
    {
        Vector3 rotation = crank.localEulerAngles;
        rotation.z = angleLimit;
        crank.localRotation = Quaternion.Euler(rotation);

        clickable = true;
    }

    private void OnMouseDown()
    {
        mousePosition = Input.mousePosition;
    }

    private void OnMouseDrag()
    {
        if (!clickable) return;

        if (mousePosition.x != Input.mousePosition.x)
        {
            float xDiff = mousePosition.x - Input.mousePosition.x;
            Vector3 rotation = crank.localEulerAngles;
            rotation.z += xDiff * crankMoveMultiplier;

            rotation.z = Mathf.Clamp(rotation.z, 0f, angleLimit * 2);

            crank.localRotation = Quaternion.Euler(rotation);
        }
        mousePosition = Input.mousePosition;
    }

    /// <summary>
    /// Get value from crank. Value will be linearly interpolated between min and max.
    /// </summary>
    /// <param name="min">Min value will be achieved when turning the crank completely to the left.</param>
    /// <param name="max">Max value will be achieved when turning the crank completely to the right.</param>
    /// <returns></returns>
    public float GetCurrentValue(int min, int max)
    {
        return Mathf.Lerp(max, min, crank.localEulerAngles.z / (angleLimit * 2));
    }

    /// <summary>
    /// Rotate the crank to some percentage-based rotation. 0 is completely to the left, 
    /// while 1 is completely turned to the right.
    /// Disables rotating the crank by using the mouse.
    /// </summary>
    /// <param name="anglePercent">The percentage of the maximum possible angle.</param>
    /// <param name="seconds">The amount of seconds, over which the rotation will be performed.</param>
    public void RotateTowardsOver(float anglePercent, float seconds)
    {
        float targetAngle = (1 - anglePercent) * angleLimit * 2;
        float startAngle = crank.localEulerAngles.z;
        StartCoroutine(DoRotate(startAngle, targetAngle, seconds));
    }

    private IEnumerator DoRotate(float startAngle, float targetAngle, float seconds)
    {
        clickable = false;

        float t = 0;
        float secondsPassed = 0;
        float timeStep = 0.1f;
        float step = 1 / (seconds / timeStep);

        while (secondsPassed <= seconds)
        {
            float newAngle = Mathf.Lerp(startAngle, targetAngle, t);
            Vector3 newRotation = crank.localEulerAngles;
            newRotation.z = newAngle;
            crank.localEulerAngles = newRotation;

            t += step;
            secondsPassed += timeStep;
            yield return new WaitForSeconds(timeStep);
        }

        clickable = true;
    }
}
