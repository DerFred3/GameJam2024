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
    private float _quotient;
    
    private int direction = 1;
    
    private bool isStatic = false;
    
    void Start()
    {
        if (point1.Equals(point2))
        {
            isStatic = true;
        }

        _quotient = Vector2.Distance(point1, transform.position) / Vector2.Distance(point1, point2);
        currentPosition = _quotient;
        sinus = _quotient;
    }

    private float sinus = 0;
    private void Update()
    {
        if (isStatic)
            return;
        
        switch (movementType)
        {
            case MovementTypes.Linear:
                currentPosition += BASE_SPEED * speed * direction * Time.deltaTime;
        
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
                sinus += BASE_SPEED * speed * Time.deltaTime;
                var lerpValue = (float)Math.Abs(LerpValue(sinus));
                transform.position = Vector2.Lerp(point1, point2, lerpValue);
                break;
        }
    }
    
    public static double CustomMod(double x, double y)
    {
        return x - y * Math.Floor(x / y);
    }
    
    private double LerpValue(float i)
    {
        double degree = CustomMod(i, Math.PI * 2);
        var sineValue = Math.Sin(degree);
        return 0.5* (1 + sineValue);
    }
    
}
