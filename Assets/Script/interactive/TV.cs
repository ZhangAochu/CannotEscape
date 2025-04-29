using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(DialogueController))]
public class TV : MonoBehaviour
{
    private DialogueController dialogueController;
    private Doctor doctor;

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
        if (scene.name == "Sickroom TV")
        {
            Action callback = () =>
            {
                GlobalDoctorState.Instance.doctorIsAwake = true;
            };
            DialogueEmpty(callback);
        }
    }

    public void DialogueEmpty(Action callback)
    {
        dialogueController.ShowDialogueEmpty(callback);
    }


}

