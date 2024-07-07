using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private int priority; // player get always spawned back to the checkpoint with highest priority
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var script = other.GetComponentInParent<RespawnBehaviour>();
            if (!script)
            {
                throw new MissingComponentException("Player misses RespawnBehaviour!");
            }
            script.EnterCheckpoint(priority, transform);
            GetComponent<Animator>().Play("Checkpoint");
        }
    }
}
