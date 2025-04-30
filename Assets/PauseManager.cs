using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel; // 检查此脚本的Inspector面板，并将PausePanel拖拽到此处
    private bool isPaused = false;

    void Update()
    {
        // 按下 ESC 键暂停/继续游戏
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1; // 恢复游戏时间流动
        pausePanel.SetActive(false); // 隐藏暂停菜单面板
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0; // 暂停游戏时间流动
        pausePanel.SetActive(true); // 显示暂停菜单面板
    }

    public void MainMenu()
    {
        isPaused = true;
        Time.timeScale = 1; // 暂停游戏时间流动
        SceneManager.LoadScene("Start");
    }


    public void Quit()
    {
        Application.Quit();
    }
}