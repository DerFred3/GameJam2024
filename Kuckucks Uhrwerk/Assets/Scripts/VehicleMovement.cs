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
    private float playerMoving = 1f;

    public Vector3 velocityOffset = Vector3.zero;

    private Rigidbody2D rb;
    private List<GameObject> currentlyTouching = new List<GameObject>();

    private Vector3 movementDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        MovePlatform.stopPlayerMovement.AddListener(MovePlatform_stopPlayerMovement);
        MovePlatform.startPlayerMovement.AddListener(MovePlatform_startPlayerMovement);
    }

    private void FixedUpdate()
    {
        if (currentlyTouching.Count <= 0)
        {
            // Only apply velocity offset(s)
            Vector3 velocity = rb.velocity;
            velocity += velocityOffset;
            rb.velocity = velocity;
        }
        else
        {
            speedMultiplier = crankHandle.GetCurrentValue(speedMultiplierMinimum, speedMultiplierMaximum);

            rb.velocity = (movementDirection * speedMultiplier * boostMultiplier * Time.deltaTime + velocityOffset)* playerMoving;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        movementDirection = collision.transform.right;
        movementDirection = new Vector3(Mathf.Abs(movementDirection.x), Mathf.Abs(movementDirection.y),
            Mathf.Abs(movementDirection.z));
        
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "RotatingPlatform") return;

        float angleOfPlatform = collision.transform.parent.eulerAngles.z % 360;
        if (angleOfPlatform > 80f && angleOfPlatform < 270)
        {
            currentlyTouching.Remove(collision.gameObject);
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

    private void MovePlatform_stopPlayerMovement() {
        playerMoving = 0f;
    }

    private void MovePlatform_startPlayerMovement()
    {
        playerMoving = 1f;
    }
}
