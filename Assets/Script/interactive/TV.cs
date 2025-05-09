using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(DialogueController))]
public class TV : MonoBehaviour
{
    private DialogueController dialogueController;

    private void Awake()
    {
        dialogueController = GetComponent<DialogueController>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Sickroom TV")
        {
            Action callback = () =>
            {
                // 使用安全的方法设置状态
                GlobalDoctorState.SafeSetDoctorAwake(true);

                // 等待一帧确保状态设置完成
                StartCoroutine(WaitForStateAndSwitch());
            };

            DialogueEmpty(callback);
        }
    }

    private IEnumerator WaitForStateAndSwitch()
    {
        // 等待GlobalDoctorState初始化完成
        while (!GlobalDoctorState.Instance.isInitialized)
        {
            Debug.Log("等待GlobalDoctorState初始化...");
            yield return null;
        }

        Debug.Log("GlobalDoctorState已初始化，继续执行场景切换");

        // 继续执行场景切换逻辑
        // 这里可能需要调用TransitionManager或其他方法
        TransitionManager.Instance.Transition("Sickroom TV", "Sickroom");
    }

    public void DialogueEmpty(Action callback)
    {
        dialogueController.ShowDialogueEmpty(callback);
    }
}