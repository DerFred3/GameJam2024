using System;
using UnityEngine;

enum MovementTypes
{
    Linear, 
    EaseInEaseOut,
}

public class Chainsaw : MonoBehaviour
{
    [SerializeField] private Vector2 point1, point2;

    [SerializeField] private float speed;
    [SerializeField] private MovementTypes movementType;
    
    private const float BASE_SPEED = 0.001f;
    private float currentPosition;
    private float deaccelerationSpeed;
    
    private int direction = 1;
    
    private bool isStatic = false;
    
    void Start()
    {
        transform.position = point1;
        if (point1.Equals(point2))
        {
            isStatic = true;
        } 
    }

    private float sinus = 0;
    private void Update()
    {
        if (isStatic)
            return;
        
        switch (movementType)
        {
            case MovementTypes.Linear:
                currentPosition += BASE_SPEED * speed * direction;
        
                if (currentPosition >= 1)
                {
                    direction = -1;
                } else if (currentPosition <= 0)
                {
                    direction = 1;
                }
                transform.position = Vector2.Lerp(point1, point2, currentPosition);
                break;
            case MovementTypes.EaseInEaseOut:
                sinus += BASE_SPEED * speed;
                transform.position = Vector2.Lerp(point1, point2, (float)Math.Abs(LerpValue(sinus))); //currentPosition);
                break;
        }
    }

    private double LerpValue(float i)
    {
        double degree = i % (Math.PI * 2);
        return 0.5* (1 + Math.Sin(2 * Math.PI * degree));
    }
}
