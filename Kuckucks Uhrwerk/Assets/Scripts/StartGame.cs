using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] 
    private string gameScene;
    
    public void OnStartClicked()
    {
        SceneManager.LoadScene(gameScene);
    }
}
