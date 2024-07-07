using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform transformToFollow;
    [SerializeField] private CrankHandle speedCrank;

    private float followStiffnessMinimum = 1f;
    private float followStiffnessMaximum = 2f;

    private void Update()
    {
        float followStiffness = speedCrank.GetCurrentValue(followStiffnessMaximum, followStiffnessMinimum);
        Vector2 newPosition = Vector2.Lerp(transform.position, transformToFollow.position, Time.deltaTime * followStiffness);
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }
}
