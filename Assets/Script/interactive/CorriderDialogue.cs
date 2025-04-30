using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(DialogueController))]
public class CorriderDialogue : MonoBehaviour
{
    private DialogueController dialogueController;
    private static bool isfirstTime = false;

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

        if (scene.name == "Corridor" && !isfirstTime)
        {
            DialogueEmpty();
            isfirstTime = true;
        }
    }

    public void DialogueEmpty()
    {
        dialogueController.ShowDialogueEmpty();
    }
}