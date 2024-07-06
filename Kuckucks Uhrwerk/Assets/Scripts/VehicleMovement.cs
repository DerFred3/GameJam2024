using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    [SerializeField] private int speedMultiplierMinimum;
    [SerializeField] private int speedMultiplierMaximum;
    [SerializeField] private float boostIncreasePer;
    [SerializeField] private CrankHandle crankHandle;

    private float speedMultiplier;
    private float boostMultiplier = 0f;

    private Rigidbody2D rb;
    private List<GameObject> currentlyTouching = new List<GameObject>();

    private Vector3 movementDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (currentlyTouching.Count <= 0) return;

        speedMultiplier = crankHandle.GetCurrentValue(speedMultiplierMinimum, speedMultiplierMaximum);

        rb.velocity = movementDirection * speedMultiplier * boostMultiplier * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        movementDirection = collision.transform.right;
        currentlyTouching.Add(collision.gameObject);

        switch (collision.gameObject.tag)
        {
            case "Ground":
                boostMultiplier = 1f;
                break;
            case "Speed":
                boostMultiplier *= boostIncreasePer;
                break;
            case "Portal":
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
            case "Portal":
                break;
            default:
                Debug.Log("Collision with object with tag: `" + collision.gameObject.tag + "` for which no interaction is specified!");
                break;
        }
    }
}
