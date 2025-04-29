using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportby : Teleport
{
    [Header("Item Requirements")]
    [Tooltip("需要持有的物品ID")]
    public ItemName requiredItem; // 需要在物品栏中持有的物品ID
    public ItemName needItem;
    public static bool isGet = false;
    public bool isDone;

    /*
    * 为需要特殊物品的传送点添加对话的修改
    * @袁新坪
    */
    public DialogueData_SO successDialogue;//成功传送
    public DialogueData_SO needItemDialogue; //需要物品
    public DialogueData_SO notRequiredItemDialogue;//错误物品
    public DialogueData_SO emptyClickDialogue;//空点击对话
    public void TeleportToSceneby(ItemName itemName)
    {
        if(itemName==ItemName.None)
        {
            if (emptyClickDialogue != null && emptyClickDialogue.dialogueList.Count > 0)
            {
                DialogueController.Instance.ShowDialogueFromItem(emptyClickDialogue);
            }
            return;
        }
        // 检查物品栏中是否有需要的物品
        if (itemName == requiredItem && (isGet == true || needItem == ItemName.None) && !isDone)
        {
            isDone = true;
            if (successDialogue != null && successDialogue.dialogueList.Count > 0)
            {
                DialogueController.Instance.ShowDialogueFromItem(successDialogue, () =>
                {
                    // 调用父类的传送方法
                    base.TeleportToScene();
                    EventHandler.CallItemUsedEvent(itemName);
                });
            }
            else
            {
                // 如果没有对话数据，直接执行传送逻辑
                base.TeleportToScene();
                EventHandler.CallItemUsedEvent(itemName);
            }
        }

        else if (itemName == needItem)
        {
            EventHandler.CallItemUsedEvent(itemName);
            isGet = true;
            // 显示获得需要物品的对话
            if (needItemDialogue != null && needItemDialogue.dialogueList.Count > 0)
            {
                DialogueController.Instance.ShowDialogueFromItem(needItemDialogue);
            }
        }
        else
        {
            // 显示错误物品的对话
            if (notRequiredItemDialogue != null && notRequiredItemDialogue.dialogueList.Count > 0)
            {
                DialogueController.Instance.ShowDialogueFromItem(notRequiredItemDialogue);
            }
        }
    }
}
/*
    // 重写TeleportToScene方法，添加物品检查
    public void TeleportToSceneby(ItemName itemName)
    {
        // 检查物品栏中是否有需要的物品
        if (itemName == requiredItem && (isGet == true || needItem == ItemName.None) && !isDone)
        {
            isDone = true;
            // 调用父类的传送方法
            base.TeleportToScene();
            EventHandler.CallItemUsedEvent(itemName);
        }

        if (itemName == needItem)
        {
            EventHandler.CallItemUsedEvent(itemName);
            isGet = true;
        }
    }
*/