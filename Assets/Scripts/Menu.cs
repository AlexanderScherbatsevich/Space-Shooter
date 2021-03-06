using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject optionPanel;
    public GameObject menuPanel;

    private void Start()
    {
        Cursor.visible = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            optionPanel.SetActive(false);
            menuPanel.SetActive(true);
        }
    }

    public void StartGame()
    {
        LevelLoader.S.LoadNextLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Debug.Log("exitGame");
        Application.Quit();
    }

    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
        HighScore.score = 0;
    }
}
