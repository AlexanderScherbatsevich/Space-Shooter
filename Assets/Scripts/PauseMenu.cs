using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool Option = false;

    public GameObject pauseMenu;
    public GameObject optionPanel;
    public Text score;
    public Text level;

    void Update()
    {
        if (optionPanel.activeInHierarchy)
        {
            Option = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = false;
            if (GameIsPaused && Option) Pause();
            else if (GameIsPaused)
            {
                Cursor.visible = false;
                Resume();
            }
            else Pause();
        }

        score.text = "Score: " + GameManager.S.scoreCounter.text;
        level.text = "Level: " + GameManager.S.levelCounter.text;
    }
    public void Resume()
    {
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;   
    }
     void Pause()
    {
        Cursor.visible = true;
        pauseMenu.SetActive(true);
        optionPanel.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Option = false;
    }

    public void Exit()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetString("Score", "0");
        LevelLoader.S.LoadNextLevel(0);
    }
}
