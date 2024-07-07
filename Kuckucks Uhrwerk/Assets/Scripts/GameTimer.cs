using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private int timeLimit;
    [SerializeField] private TextMeshProUGUI timerDisplay;
    private int _elapsedTime;
    public GameObject player;
    
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
            EndSceneData.ElapsedTime = _elapsedTime;
            yield return new WaitForSeconds(1);
            timerDisplay.text = timeLimit - _elapsedTime + "s";
        }
        
        // time elapsed from here on 
        EndSceneData.DidWin = false;
        EndSceneData.ElapsedTime = timeLimit;
     //   SceneManager.LoadScene("EndScene");

     player.GetComponent<VehicleMovement>().enabled = false;
     yield return new WaitForSeconds(6);
     SceneManager.LoadScene("EndScene");
    }
}
