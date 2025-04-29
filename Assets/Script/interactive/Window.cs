using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : Interactive
{
    private DialogueController dialogueController;
    public void ShowDialoguebegin()
    {
        dialogueController.ShowDialogueEmpty();
    }
    public void ShowDialoguehas()
    {
        dialogueController.ShowDialogueHas();
    }
    protected override void OnClickedAction()
    {
        ItemName heldItem = CursorManager.Instance.CurrentItem;
        if (heldItem==requiredItem)
        {
            ShowDialoguehas();
            //µã»÷£¬Ïú»Ù³¡¾°
            this.gameObject.SetActive(false);

            isDone = true;
        }
        else
        {
            ShowDialoguebegin();
        }
    }

}
