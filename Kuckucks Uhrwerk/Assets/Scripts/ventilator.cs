using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ventilator : MonoBehaviour
{
    [SerializeField] private CrankHandle globalCrankHandle;
    [SerializeField] private Transform ventilatorOrigin;
    [SerializeField] private float ventilatorStrengthMinimum;
    [SerializeField] private float ventilatorStrengthMaximum;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            VehicleMovement movement = other.transform.root.GetComponent<VehicleMovement>();
            movement.velocityOffset = (globalCrankHandle.GetCurrentValue(ventilatorStrengthMinimum, ventilatorStrengthMaximum) * 
                (transform.position - ventilatorOrigin.position)) / (other.transform.position - ventilatorOrigin.position).magnitude;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            VehicleMovement movement = other.transform.root.GetComponent<VehicleMovement>();
            movement.velocityOffset = Vector3.zero;
        }
    }
}
