using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private Dictionary<ItemName,bool> itemAvailableDict = new Dictionary<ItemName,bool>();

    private void OnEnable()
    {
        EventHandlerUI.BeforeSceneUnloadEvent += OnBeforeSceneUnloadEvent;
        EventHandlerUI.AfterSceneUnloadEvent += OnAfterSceneUnloadEvent;
        EventHandlerUI.UpdateUIEvent += OnUpdateUIEvent;
    }

    private void OnDisable()
    {
        EventHandlerUI.BeforeSceneUnloadEvent -= OnBeforeSceneUnloadEvent;
        EventHandlerUI.AfterSceneUnloadEvent -= OnAfterSceneUnloadEvent;
        EventHandlerUI.UpdateUIEvent -= OnUpdateUIEvent;
    }



    private void OnBeforeSceneUnloadEvent()
    {
        foreach (var item in FindObjectsOfType<Item>())
        {
            if (!itemAvailableDict.ContainsKey(item.itemName))
                itemAvailableDict.Add(item.itemName, true);
        }
    }
    private void OnAfterSceneUnloadEvent()
    {
        foreach (var item in FindObjectsOfType<Item>())
        {
            if (!itemAvailableDict.ContainsKey(item.itemName))
                itemAvailableDict.Add(item.itemName, true);
            else
                item.gameObject.SetActive(itemAvailableDict[item.itemName]);
        }
    }
    private void OnUpdateUIEvent(ItemDetails itemDetails, int arg2)
    {
        if (itemDetails != null)
        {
            itemAvailableDict[itemDetails.itemName] = false; 
        }
    }
}
