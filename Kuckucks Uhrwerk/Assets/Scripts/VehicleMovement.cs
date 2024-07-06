using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float boostMultiplier;

    private float defaultSpeed;

    private Rigidbody2D rb;
    private List<GameObject> currentlyTouching = new List<GameObject>();

    private Vector3 movementDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultSpeed = speedMultiplier;
    }

    private void Update()
    {
        if (currentlyTouching.Count <= 0) return;
        rb.velocity = movementDirection * speedMultiplier * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        movementDirection = collision.transform.right;
        currentlyTouching.Add(collision.gameObject);

        switch (collision.gameObject.tag)
        {
            case "Ground":
                speedMultiplier = defaultSpeed;
                break;
            case "Speed":
                speedMultiplier *= boostMultiplier;
                break;
            default:
                Debug.Log("Collided with object with tag: `" + collision.gameObject.tag + "` for which no interaction is specified!");
                break;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        currentlyTouching.Remove(collision.gameObject);
        switch (collision.gameObject.tag)
        {
            case "Ground":
                break;
            case "Speed":
                break;
            default:
                Debug.Log("Collision with object with tag: `" + collision.gameObject.tag + "` for which no interaction is specified!");
                break;
        }
    }
}
