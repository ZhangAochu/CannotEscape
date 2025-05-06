using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Restart : MonoBehaviour
{
    private Button restartButton;

    void Start()
    {
        // 获取按钮组件
        restartButton = GetComponent<Button>();

        // 确保按钮初始状态正确
        if (restartButton != null)
        {
            ResetButtonState();
        }
    }

    void OnEnable()
    {
        // 每次脚本启用时重置按钮状态
        ResetButtonState();
    }

    // 重置按钮状态的方法
    private void ResetButtonState()
    {
        if (restartButton != null)
        {
            restartButton.interactable = true;
            restartButton.onClick.RemoveAllListeners();
            restartButton.onClick.AddListener(RestartGame);
        }
    }

    public void RestartGame()
    {
        // 禁用按钮防止多次点击
        if (restartButton != null)
        {
            restartButton.interactable = false;
        }

        // 重置分数
        Mole.score = 0;

        // 使用协程异步加载场景，确保完全重置
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        // 异步加载场景
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Lab203 Boss Fight");

        // 禁止自动激活场景
        asyncLoad.allowSceneActivation = false;

        // 等待加载进度达到90%
        while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }

        // 这里可以添加加载完成前的额外重置逻辑

        // 激活场景
        asyncLoad.allowSceneActivation = true;

        // 等待场景完全加载
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // 场景加载完成后，按钮状态会在OnEnable中自动重置
    }

    // 当场景中有多个EventSystem时处理
    void CheckEventSystem()
    {
        EventSystem[] eventSystems = FindObjectsOfType<EventSystem>();
        if (eventSystems.Length > 1)
        {
            // 保留第一个，销毁其他的
            for (int i = 1; i < eventSystems.Length; i++)
            {
                Destroy(eventSystems[i].gameObject);
            }
        }
    }
}