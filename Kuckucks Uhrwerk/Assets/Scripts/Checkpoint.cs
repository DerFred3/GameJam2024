using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private int priority; // player get always spawned back to the checkpoint with highest priority
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var script = other.GetComponent<RespawnBehaviour>();
            if (!script)
            {
                throw new MissingComponentException("Player misses RespawnBehaviour!");
            }
            script.EnterCheckpoint();
        }
    }
}
