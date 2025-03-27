using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(DialogueController))] 
public class Character0 : MonoBehaviour
{
    private DialogueController dialogueController;

    private static bool hasPangbaiTriggered = false;
    private void Awake()
    {
        dialogueController = GetComponent<DialogueController>();
    }
    private void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Sickroom" && !hasPangbaiTriggered)
        {
            DialogueEmpty();
            hasPangbaiTriggered = true;
        }
    }
    public void DialogueEmpty()
    {
        dialogueController.ShowDialogueEmpty();
    }


}

    