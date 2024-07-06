using System;
using System.Collections;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;

public class Laser : MonoBehaviour
{
    [SerializeField] private float onTime, offTime;
    [SerializeField] private Vector2 point1, point2;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationAmount;
    
    private LineRenderer _lineRenderer;
    private bool _active;
    private bool _static;
    
    private void Start()
    {
        _static = point1.Equals(point2);
        _lineRenderer = GetComponent<LineRenderer>();

        RefreshLineRenderer();        
        StartCoroutine(LaserBehaviour());
    }

    private void RefreshLineRenderer()
    {
        var hit = Physics2D.Raycast(transform.position, CalculateDirectionVector());
        _lineRenderer.SetPosition(0, transform.position);

        if (!hit.transform)
        {
            _lineRenderer.SetPosition(1, (Vector2)transform.position + CalculateDirectionVector() * 100);
        }
        else
        {
            _lineRenderer.SetPosition(1, hit.point);
        }
    }
    
    private Vector2 CalculateDirectionVector()
    {
        float rotationZRad = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
        return new Vector2(Mathf.Sin(rotationZRad), -Mathf.Cos(rotationZRad));
    }
    
    private IEnumerator LaserBehaviour()
    {
        while (true)
        {
            _active = false;
            _lineRenderer.enabled = false;    
            yield return new WaitForSeconds(offTime);
            
            RefreshLineRenderer();
            _lineRenderer.enabled = true;
            _active = true;
            yield return new WaitForSeconds(onTime);
        }
    }

    private float _sinus;
    private void Update()
    {
        const float BASE_SPEED = .2f;
        double LerpValue(float i)
        {
            var degree = i % (Math.PI * 2);
            return 0.5* (1 + Math.Sin(2 * Math.PI * degree));
        }

        _sinus += BASE_SPEED * moveSpeed * Time.deltaTime;

        if (rotationAmount != 0)
        {
            float rotation = Mathf.Lerp(-rotationAmount / 2, rotationAmount / 2, (float) Math.Abs(LerpValue(_sinus)));
            transform.SetPositionAndRotation(transform.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotation));
        }
        
        if (_static)
            return;
        
        transform.position = Vector2.Lerp(point1, point2, (float)Math.Abs(LerpValue(_sinus)));
        _lineRenderer.SetPosition(0, transform.position);
    }

    private void FixedUpdate()
    {
        if (!_active)
            return;
        
        var hit = Physics2D.Raycast(transform.position, CalculateDirectionVector());
            
        if (!hit.transform) // nothing hit, shoot laser in direction
        {
            _lineRenderer.SetPosition(1, (Vector2) transform.position + CalculateDirectionVector() * 100);
            return;
        }
        
        if (hit.transform.CompareTag("Player"))
        {
            OnPlayerHit();
        }
        else
        {
            if (!_lineRenderer.GetPosition(1).Equals(hit.point)) // update laser destination if target changed
                _lineRenderer.SetPosition(1, hit.point);
        }
    }

    private void OnPlayerHit()
    {
        throw new NotImplementedException("Laser hit player is not implemented!");
    }
}
