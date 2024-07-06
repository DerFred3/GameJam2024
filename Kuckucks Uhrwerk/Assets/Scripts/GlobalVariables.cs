using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    
    public float globalRotation = 0.0f;
    public float globalRotationScale = 10.0f;
    private CrankHandle crankHandle;
    // Start is called before the first frame update
    void Start()
    {
        crankHandle = GameObject.Find("Crank").GetComponent<CrankHandle>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate(){
        globalRotation = crankHandle.GetCurrentValue(-10, 10);
        //if(globalRotation >= 90.0f){
        //    globalRotation = -90.0f;
        //}
        //globalRotation += 1f;
       
    }
}
