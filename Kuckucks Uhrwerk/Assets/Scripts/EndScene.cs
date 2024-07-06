using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndSceneData
{
    public static bool DidWin;
    public static int ElapsedTime;
}

public class EndScene : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI label;

    private void Start()
    {
        if (EndSceneData.DidWin)
        {
            label.text = "You won in " + EndSceneData.ElapsedTime + " seconds!";
        }
        else
        {
            label.text = "You ran out of time and got caught by the KUCKUCK";
        }
    }
}
