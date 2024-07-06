using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateOneSide : MonoBehaviour
{
    public bool leftSide = true;
    public Vector3 objectToRotate;

    private GlobalVariables globalVariables;

    public Vector3 rotationAxis;
    Vector3 direction;

    public
    // Start is called before the first frame update
    void Start()
    {
        objectToRotate = leftSide? transform.position - transform.right* transform.localScale.x/2 : transform.position + transform.right* transform.localScale.x / 2;
        direction = transform.position - objectToRotate;
        globalVariables = GameObject.Find("GameState").GetComponent<GlobalVariables>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){

        Quaternion rot = Quaternion.AngleAxis(globalVariables.globalRotationScale*globalVariables.globalRotation, new Vector3(0.0f,0.0f,1.0f));
        transform.position = objectToRotate + rot * direction;
        transform.localRotation = rot;

        //transform.RotateAround(objectToRotate, Vector3.forward, globalVariables.globalRotationScale * globalVariables.globalRotation);
        //transform.Rotate(0.0f, 0.0f, globalVariables.globalRotationScale * globalVariables.globalRotation, Space.World);

       /*if (transform.eulerAngles.z > 90.0f)
        {
            transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            transform.eulerAngles.y,
            90.0f
            );
        }
        if (transform.eulerAngles.z < -90.0f)
        {
            transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            transform.eulerAngles.y,
            -90.0f
            );
        }*/
    }
}
