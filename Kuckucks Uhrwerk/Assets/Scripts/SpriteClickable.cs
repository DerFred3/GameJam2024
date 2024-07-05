using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteClickable : MonoBehaviour
{
    private SpriteRenderer renderer;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseEnter()
    {
        
    }
}
