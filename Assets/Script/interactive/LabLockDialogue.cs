using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Sprites;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(DialogueController))]
public class LabLockDialogue : MonoBehaviour
{
    private DialogueController dialogueController;
    private static bool isfirsttime = true;
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
        if (scene.name == "Lab203 Coded Lock"&&isfirsttime)
        {
            isfirsttime = false;
            DialogueEmpty();
            
        }
    }

    public void DialogueEmpty()
    {
        dialogueController.ShowDialogueEmpty();
    }


}

