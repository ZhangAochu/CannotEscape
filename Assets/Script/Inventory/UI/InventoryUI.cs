using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Button leftButton, rightButton;
    public SlotUI slotUI;
    public static InventoryUI Instance;

    public int currentIndex;

    private void OnEnable()
    {
        EventHandler.UpdateUIEvent += OnUpdateUIEvent;
    }

    private void OnDisable()
    {
        EventHandler.UpdateUIEvent -= OnUpdateUIEvent;
    }

    private void OnUpdateUIEvent(ItemDetails itemDetails,int index)
    {
        var itemList = InventoryManager.Instance.itemList;
        if (itemDetails == null)
        {
            slotUI.SetEmpty();
            currentIndex = -1;
            leftButton.interactable = false;
            rightButton.interactable = false;
        }
        else
        {
            currentIndex = index;
            slotUI.SetItem(itemDetails);

            if(index > 0)
            {
                leftButton.interactable = true;
            }
            if(index < itemList.Count - 1)
            {
                rightButton.interactable = true;
            }
            if(index == -1)
            {
                leftButton.interactable = false;
                rightButton.interactable = false;
            }
        }

        
    }
    public void SwitchItem(int amount)
    {

        var index = currentIndex + amount;
        var itemList = InventoryManager.Instance.itemList;

        if (index <= 0 && itemList.Count > 1)
        {
            leftButton.interactable = false;
            rightButton.interactable = true;
        }
        else if(index >= itemList.Count - 1 && itemList.Count > 1)
        {
            leftButton.interactable = true;
            rightButton.interactable = false;
        }
        else if (itemList.Count<=1)
        {
            leftButton.interactable = false;
            rightButton.interactable = false;
        }
        else
        {
            leftButton.interactable = true;
            rightButton.interactable = true;
        }

        EventHandler.CallChangeItemEvent(index);
    }

}
