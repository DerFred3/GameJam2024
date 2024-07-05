using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateOneSide : MonoBehaviour
{
    public bool leftSide = true;
    public Vector3 objectToRotate;
    public 
    // Start is called before the first frame update
    void Start()
    {
        objectToRotate = leftSide? transform.position - transform.right*2 : transform.position + transform.right*2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        float angle = 100* Time.deltaTime;
        if (angle > 90.0){
            angle = angle - 180.0f;
        }
        Debug.Log(angle);
        transform.RotateAround(objectToRotate, Vector3.forward, angle);
    }
}
