using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ventilator : MonoBehaviour
{
    [SerializeField] private Transform ventilatorOrigin;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Penis");
            VehicleMovement movement = other.transform.root.GetComponent<VehicleMovement>();
            movement.velocityOffset = 1f*(transform.position - ventilatorOrigin.position);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Penis out!");
            VehicleMovement movement = other.transform.root.GetComponent<VehicleMovement>();
            movement.velocityOffset = Vector3.zero;
        }
    }
}
