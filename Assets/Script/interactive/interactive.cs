using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Interactive : MonoBehaviour
{
    public ItemName requiredItem;
    public ItemName rewardItem;
    public bool ifDistory = true;

    public bool isDone;

    public void CheckItem(ItemName itemName)
    {
        OnClickedAction();
        if (itemName == requiredItem&&!isDone)
        {
            isDone = true;
            //使用物品,移除物品
            if(ifDistory)
            EventHandler.CallItemUsedEvent(itemName);
        }
    }

    /// <summary>
    /// 默认物品正确时的交互
    /// </summary>
    protected virtual void OnClickedAction()
    {

    }

    public  void EmptyClicked()
    {
        if (requiredItem == ItemName.None)
        {
            Debug.Log("Empty Clicked");
        }
        else
        {
            OnClickedAction();
        }
    }
}
