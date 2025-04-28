using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(DialogueController))]
public class Doctor : Interactive
{
    private BoxCollider2D coll;

    private DialogueController dialogueController;
    bool hasfinish = false;


    private void Awake()
    {
        dialogueController = GetComponent<DialogueController>();

        coll = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadEvent;
    }

    private void OnDisable()
    {
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadEvent;
    }

    private void OnAfterSceneLoadEvent()
    {
        if (!isDone)
        {
            // 场景加载后重置状态（如果需要）
            coll.enabled = true;
        }
        else
        {
            coll.enabled = false;
        }
    }    
    public void ShowDialoguebegin()
    {
        dialogueController.ShowDialogueEmpty();
    }
    public void ShowDialoguehas()
    {
        dialogueController.ShowDialogueHas();
    }
    public void ShowDialoguefinish()
    {
        dialogueController.ShowDialogueFinish();
    }
    protected override void OnClickedAction()
    {
        if (hasfinish)
        {
            ShowDialoguefinish();
        }
        else
        {
            if (InventoryManager.Instance.HasItem(ItemName.Noodle))
            {
                ShowDialoguehas();
                hasfinish = true;
                InventoryManager.Instance.AddItem(rewardItem);
            }
            else
            {
                ShowDialoguebegin();
            }
        }
        //给予病历
       // InventoryManager.Instance.AddItem(rewardItem);

        // 关闭碰撞体
        //coll.enabled = false;

        // 标记为已完成
        //isDone = true;
    }
}


