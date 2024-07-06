using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    private static bool _hasShown;
    
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image speedCrank, globalCrank;
    
    private const string SpeedCrank = "Use this crank to controll the velocity.";
    private const string GlobalCrank = "Use this crank to interact with objects on the route.";
    private const string Goal = "Reach the end before the Kuckuck wakes up and gets you.";
    private const string GoodLuck = "Good luck!";

    private bool _highlightSpeedCrank;
    private bool _highlightGlobalCrank;
    
    void Start()
    {
        text.text = "";
        if (!_hasShown)
            StartCoroutine(ShowTutorial());
    }

    private IEnumerator ShowTutorial() {
        const float typeSpeed = .1f;
        
        string label = "";

        for (int i = 0; i < SpeedCrank.Length; i++)
        {
            _highlightSpeedCrank = true;
            label += SpeedCrank[i];
            yield return new WaitForSeconds(typeSpeed);
            
            text.text = label;
        }

        yield return new WaitForSeconds(2);

        _highlightSpeedCrank = false;
        speedCrank.color = Color.white;
        label = "";
        text.text = "";
        
        for (int i = 0; i < GlobalCrank.Length; i++)
        {
            _highlightGlobalCrank = true;
            label += GlobalCrank[i];
            yield return new WaitForSeconds(typeSpeed);
            
            text.text = label;
        }
        
        yield return new WaitForSeconds(2);

        _highlightGlobalCrank = false;
        globalCrank.color = Color.white;
        label = "";
        text.text = "";
        
        for (int i = 0; i < Goal.Length; i++)
        {
            label += Goal[i];
            yield return new WaitForSeconds(typeSpeed);
            
            text.text = label;
        }

        yield return new WaitForSeconds(2);
        
        label = "";
        text.text = "";
        
        for (int i = 0; i < GoodLuck.Length; i++)
        {
            label += GoodLuck[i];
            yield return new WaitForSeconds(typeSpeed);
            
            text.text = label;
        }

        yield return new WaitForSeconds(2);
        text.text = "";
    }

    private float _currentColor;
    private float _direction;
    private void Update()
    {
        if (_currentColor >= 1)
        {
            _direction = -1;
        }
        if (_currentColor <= 0)
        {
            _direction = 1;
        }

        _currentColor += _direction * 0.005f;
        
        if (_highlightSpeedCrank)
        {
            speedCrank.color = Color.Lerp(Color.white, Color.red, _currentColor);
        }

        if (_highlightGlobalCrank)
        {
            globalCrank.color = Color.Lerp(Color.white, Color.red, _currentColor);
        }
    }
}
