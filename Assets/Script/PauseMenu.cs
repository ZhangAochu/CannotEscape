using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI; 
    // Start is called before the first frame update
    void Start()
    {
        Resume();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        GameIsPaused = false;
        Time.timeScale = 1.0f;
    }

    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        GameIsPaused = true;
        Time.timeScale = 0.0f;
    }

    public void MainMenu()
    {
        GameIsPaused = false;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Menu");
    }
}
