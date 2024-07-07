using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovePlatform : MonoBehaviour
{
    [SerializeField] private Transform platform;
    [SerializeField] private CrankHandle globalCrank;
    [SerializeField] private float minMovement, maxMovement;

    private float offset = 0;

    private bool playerIsOnPlatform = false;
    private Vector3 minPosition, maxPosition;

    private GameObject player = null;

    public static UnityEvent stopPlayerMovement = new UnityEvent(); 
    public static UnityEvent startPlayerMovement = new UnityEvent();

    private void Start()
    {
        minPosition = transform.position - new Vector3(minMovement,0,0);
        maxPosition = transform.position + new Vector3(maxMovement,0,0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(minPosition, maxPosition, globalCrank.GetCurrentValue(0, 1));
        if (playerIsOnPlatform) {
            player.transform.position = new Vector3(transform.position.x+offset,player.transform.position.y,player.transform.position.z);
        }
        if (globalCrank.GetCurrentValue(0, 1) >= 0.95) {
            startPlayerMovement.Invoke();
            playerIsOnPlatform = false;
        }
    }

    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") {

            stopPlayerMovement.Invoke();
            player = collision.gameObject;
            offset = collision.transform.position.x - transform.position.x;
            playerIsOnPlatform = true;
        }
    }
}
