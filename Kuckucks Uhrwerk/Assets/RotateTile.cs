using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTile : MonoBehaviour
{
    public int rotationAngle;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.eulerAngles = new Vector3(0,0,GlobalVariables.globalRotation);
    }
}
