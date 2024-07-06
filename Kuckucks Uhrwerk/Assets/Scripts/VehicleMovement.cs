using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    [SerializeField] private float speedMultiplier;

    private Rigidbody2D rb;
    private List<GameObject> currentlyTouching = new List<GameObject>();

    private Vector3 movementDirection;

    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (currentlyTouching.Count <= 0) return;

        transform.position += movementDirection * speedMultiplier * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Ground":
                currentlyTouching.Add(collision.gameObject);
                movementDirection = collision.transform.right.normalized;
                rb.isKinematic = true;
                break;
            default:
                Debug.Log("Collided with object with tag: `" + collision.gameObject.tag + "` for which no interaction is specified!");
                break;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Ground":
                currentlyTouching.Remove(collision.gameObject);
                rb.isKinematic = false;
                break;
            default:
                Debug.Log("Collision with object with tag: `" + collision.gameObject.tag + "` for which no interaction is specified!");
                break;
        }
    }
}
