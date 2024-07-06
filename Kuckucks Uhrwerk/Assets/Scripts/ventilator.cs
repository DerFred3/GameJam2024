using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ventilator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Penis");
            Rigidbody2D rb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
            rb.velocity += 100f*(Vector2)(other.transform.position - transform.position);
        }
           
    }
}
