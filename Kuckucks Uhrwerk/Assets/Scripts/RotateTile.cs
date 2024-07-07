using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTile : MonoBehaviour
{
    public int rotationAngle;
    private GlobalVariables globalVariables;
    // Start is called before the first frame update
    void Start()
    {
        globalVariables = GameObject.Find("GameState").GetComponent<GlobalVariables>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.eulerAngles = new Vector3(0,0, globalVariables.globalRotationScale*globalVariables.globalRotation+rotationAngle);
    }
}
