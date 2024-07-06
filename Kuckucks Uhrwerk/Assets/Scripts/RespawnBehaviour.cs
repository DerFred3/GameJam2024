using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Int32;

public class RespawnBehaviour : MonoBehaviour
{
    [SerializeField] private Transform startPosition;
    
    private Transform _currentSpawnPosition;
    private int _currentCheckpointPriority;
    private int _attempts;
    
    // Start is called before the first frame update
    void Start()
    {
        _currentSpawnPosition = startPosition;
        _currentCheckpointPriority = MinValue;
    }

    public void EnterCheckpoint(int priority, Transform position)
    {
        if (priority < _currentCheckpointPriority)
            return;

        _currentCheckpointPriority = priority;
        _currentSpawnPosition = position;
    }

    public void OnDeath()
    {
        _attempts++;
        transform.position = _currentSpawnPosition.position;
    }
}
