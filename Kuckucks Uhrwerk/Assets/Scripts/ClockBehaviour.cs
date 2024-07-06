using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class ClockBehaviour : MonoBehaviour
{
    [SerializeField] GameObject clock, hourHand, minuteHand;
    int clockCounter, stepCounter;
    float hourDegree, minuteDegree;
    private void Start()
    {
        initializeClock(0, 10);
    }


    //Initialize Clock at 11pm
    public void initializeClock(int start, int maxTime) {
        clockCounter = 0;
        minuteDegree = start * 30;
        hourDegree = 11 * 30;
        hourHand.transform.RotateAround(clock.transform.position, new Vector3(0, 0, -1), hourDegree);
        minuteHand.transform.RotateAround(clock.transform.position, new Vector3(0,0,-1), minuteDegree);
        StartCoroutine(moveClock(maxTime));
    }

    private IEnumerator moveClock(int maxTime) {
        while (clockCounter < maxTime)
        {
            clockCounter++;
            minuteDegree = 360/maxTime;
            hourDegree = 30/maxTime;
            yield return moveStep(minuteDegree, hourDegree);
        }
    }

    private IEnumerator moveStep(float minuteDegree, float hourDegree) {
        minuteDegree = minuteDegree / 30;
        hourDegree =  0.3f / hourDegree;
        while (stepCounter < 30)
        {
            stepCounter++;
            minuteHand.transform.RotateAround(clock.transform.position, new Vector3(0, 0, -1), minuteDegree);
            hourHand.transform.RotateAround(clock.transform.position, new Vector3(0, 0, -1), hourDegree);
            yield return new WaitForSeconds(0.03333f);
        }
        stepCounter = 0;
    }
}
