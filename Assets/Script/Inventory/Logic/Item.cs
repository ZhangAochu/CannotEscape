using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemName itemName;
    public DialogueData_SO dialogueData;
    public void ItemClicked()
    {
        if (dialogueData != null && dialogueData.dialogueList.Count > 0)
        {
            // 定义回调函数
            Action callback = () =>
            {
                InventoryManager.Instance.AddItem(itemName);
                this.gameObject.SetActive(false);
            };

            // 调用 ShowDiaslogueFromItem 方法并传入回调函数
            DialogueController.Instance.ShowDialogueFromItem(dialogueData, callback);
        }
        else
        {
            // 如果没有对话数据，直接执行后续逻辑
            InventoryManager.Instance.AddItem(itemName);
            this.gameObject.SetActive(false);
        }
    }
}