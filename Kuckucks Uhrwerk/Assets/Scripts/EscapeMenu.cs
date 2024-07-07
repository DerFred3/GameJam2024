using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EscapeMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] toActivateGameObjects;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private int firstLevelBuildIndex;

    private bool menuActive = false;

    private void Start()
    {
        DeactivateEscapeMenu();

        resumeButton.onClick.AddListener(ResumeButton_OnClick);
        restartButton.onClick.AddListener(RestartButton_OnClick);
        quitButton.onClick.AddListener(QuitButton_OnClick);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuActive)
            {
                DeactivateEscapeMenu();
            } 
            else
            {
                ActivateEscapeMenu();
            }
        }
    }

    public void ActivateEscapeMenu()
    {
        Time.timeScale = 0f;

        foreach (GameObject gameObject in toActivateGameObjects)
        {
            gameObject.SetActive(true);
        }
        menuActive = true;
    }

    public void DeactivateEscapeMenu()
    {
        Time.timeScale = 1f;

        foreach (GameObject gameObject in toActivateGameObjects)
        {
            gameObject.SetActive(false);
        }
        menuActive = false;
    }

    private void ResumeButton_OnClick()
    {
        DeactivateEscapeMenu();
    }

    private void RestartButton_OnClick()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(firstLevelBuildIndex);
    }

    private void QuitButton_OnClick()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
    }
}
