using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarBehaviour : MonoBehaviour
{
    private bool isDriving = false;
    private float speed = 2f;

    private Rigidbody2D rb;

    private Collider2D colliders;

    [SerializeField] LayerMask groundLayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isDriving=true;
        StartCoroutine(moveCar());
    }
    private IEnumerator moveCar() {
        while (isDriving)
        {
            rb.velocity = Vector2.right * speed * 3;
            yield return new WaitForSeconds(0.1f);
            if (!isOnGround())
            {
                rb.freezeRotation = true;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));            }
            else { 
                rb.freezeRotation=false;
            }
        }
    }

    public void StartDriving() {
        isDriving = true;
        StartCoroutine(moveCar());
    }

    public void StopDriving() {
        isDriving = false;
    }

    public void changeSpeed(float speed) {
        this.speed = speed;
    }

    private bool isOnGround() {
        colliders = Physics2D.OverlapBox(transform.position, new Vector2(2.575f,1.1f), Quaternion.identity.z, groundLayer);
        if (colliders != null)
        {
            colliders = null;
            return true;
        }
        else {
            colliders = null;
            return false;
        }
    }
}
