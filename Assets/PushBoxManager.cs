using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PushBoxManager : MonoBehaviour
{
    public int totalBox;
    public int finishedBox;

    public GameObject victoryPanel;
    public GameObject teleport;
    public GameObject backButton;

    private void Start()
    {
        victoryPanel.SetActive(false);
        teleport.SetActive(false);
        backButton.SetActive(false);
    }

    private void Update()
    {
        // 新增ESC按键检测
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReStartGame();
        }
    }

    public void CheckFinish()
    {
        if(finishedBox == totalBox)
        {
            EndGame();
        }
    }
    
    private void EndGame()
    {
        victoryPanel.SetActive(true);
        teleport.SetActive(true);
        backButton.SetActive(true);
    }

    private void ReStartGame()
    {

        // 确保主相机不会被销毁
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("MainCamera"));

        // 重新加载当前场景
        SceneManager.UnloadSceneAsync("Sickroom Push Box");
        SceneManager.LoadScene("Sickroom Push Box", LoadSceneMode.Additive);
    }

}
