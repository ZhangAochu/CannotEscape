using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DialogueController : MonoBehaviour
{
    public DialogueData_SO dialogueEmpty;
    
    public DialogueData_SO dialogueFinish;
    
    private Stack<string> Stack;

    private Stack<string> dialogueEmptyStack;

    private Stack<string> dialogueFinishStack;

    private Coroutine currentRoutine;

    private bool isTalking;

    private void Awake()
    {
        FillDialogueStack();
    }
    private void FillDialogueStack()
    {
        dialogueEmptyStack = new Stack<string>();
        dialogueFinishStack = new Stack<string>();

        for (int i=dialogueEmpty.dialogueList.Count-1; i >=0; i--)
        {
            dialogueEmptyStack.Push(dialogueEmpty.dialogueList[i]);
        }

        for(int i=dialogueFinish.dialogueList.Count - 1; i >= 0; i--)
        {
            dialogueFinishStack.Push(dialogueFinish.dialogueList[i]);
        }   
    }
    public void ShowDialogueEmpty()
    {
        if (!isTalking)
        {
            StartCoroutine(DialogueRoutine(dialogueEmptyStack));
        }
    }

    public void ShowDialogueFinish()
    {
        if (!isTalking)
        {
            StartCoroutine(DialogueRoutine(dialogueFinishStack));
        }
    }

    private void Update()
    {
        if (isTalking && Input.GetMouseButtonDown(0))
        {
            if(currentRoutine != null)
            {
                StopCoroutine(currentRoutine);
                currentRoutine = null;
                EventHandler.CallDialogueContinueEvent();
            }
        }
    }
    private IEnumerator DialogueRoutine(Stack<string> data)
    {
        isTalking = true;  
        EventHandler.CallGameStateChangeEvent(GameState.Pause);

        while(data.Count > 0)
        {
            if(data.TryPop(out string result))
            {
                EventHandler.CallShowDialogueEvent(result);

                var continueFlag = new bool[1] { false };
                Action callback = () => continueFlag[0] = true;

                EventHandler.OnDialogueContinue += callback;
                yield return new WaitUntil(() => continueFlag[0]);
                EventHandler.OnDialogueContinue -= callback;
            }
            yield return new WaitForSeconds(0.1f);
        }
        EventHandler.CallShowDialogueEvent(string.Empty);
        FillDialogueStack();  
        isTalking = false;
        EventHandler.CallGameStateChangeEvent(GameState.Gameplay);
    }

    private IEnumerator WaitForClick()
    {
        while(!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
    }
}
