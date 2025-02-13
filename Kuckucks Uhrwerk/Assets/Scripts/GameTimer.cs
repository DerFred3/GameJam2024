using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private int timeLimit;
    [SerializeField] private TextMeshProUGUI timerDisplay;
    private int _elapsedTime;
    
    // Start is called before the first frame update
    void Start()
    {
        timerDisplay.text = timeLimit.ToString() + "s";
        StartCoroutine(StartTimer());
        _elapsedTime = 0;
    }

    private IEnumerator StartTimer()
    {
        while (_elapsedTime++ < timeLimit)
        {
            yield return new WaitForSeconds(1);
            timerDisplay.text = timeLimit - _elapsedTime + "s";
        }
        
        // time elapsed from here on 
        EndSceneData.DidWin = false;
        EndSceneData.ElapsedTime = timeLimit;
        SceneManager.LoadScene("EndScene");
    }
}
