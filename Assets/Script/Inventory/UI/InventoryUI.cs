using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Button leftButtton, rightButtton;
    public SlotUI slotUI;

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
        if (itemDetails == null)
        {
            slotUI.SetEmpty();
            currentIndex = -1;
            leftButtton.interactable = false;
            rightButtton.interactable = false;
        }
        else
        {
            currentIndex = index;
            slotUI.SetItem(itemDetails);

            if(index > 0)
            {
                leftButtton.interactable = true;
            }
            if(index == -1)
            {
                leftButtton.interactable = false;
                rightButtton.interactable = false;
            }
        }

        
    }
    public void SwitchItem(int amount)
    {
        var index = currentIndex + amount;

        if(index < currentIndex)
        {
            leftButtton.interactable = false;
            rightButtton.interactable = true;
        }
        else if(index > currentIndex)
        {
            leftButtton.interactable = true;
            rightButtton.interactable = false;
        }
        else
        {
            leftButtton.interactable = true;
            rightButtton.interactable = true;
        }

        EventHandler.CallChangeItemEvemt(index);
    }

}
