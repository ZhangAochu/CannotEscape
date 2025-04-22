using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    public ItemDataList_SO itemData;
    public List<ItemName> itemList = new List<ItemName>();

    private void OnEnable()
    {
        EventHandler.ChangeItemEvent += OnChangeItemEvent;
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
        EventHandler.ItemUsedEvent += OnItemUsedEvent;
    }

    private void OnDisable()
    {
        EventHandler.ChangeItemEvent -= OnChangeItemEvent;
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
        EventHandler.ItemUsedEvent -= OnItemUsedEvent;
    }
    private void OnChangeItemEvent(int index)
    {
        if(index >= 0 && index < itemList.Count)
        {
            ItemDetails item = itemData.GetItemDetails(itemList[index]);
            EventHandler.CallUpdateUIEvent(item, index);

        }

    }

    private void OnAfterSceneLoadedEvent()
    {
        if (itemList.Count == 0)
            EventHandler.CallUpdateUIEvent(null, 1);
        else
        {
            for(int i = 0; i < itemList.Count; i++)
            {
                EventHandler.CallUpdateUIEvent(itemData.GetItemDetails(itemList[i]), i);
            }
        }
    }

    private void OnItemUsedEvent(ItemName itemName)
    {
        var index = GetItemIndex(itemName);
        itemList.RemoveAt(index);
        //todo:暂时实现物品使用效果
        if (itemList.Count == 0)
        {
            EventHandler.CallUpdateUIEvent(null, -1);
        }
        else
        {
            EventHandler.CallUpdateUIEvent(itemData.GetItemDetails(itemList[0]),0);
        }
            
    }

    private void Awake()
    {
        // 确保 itemData 不为 null
        if (itemData == null)
        {
            Debug.LogError("itemData is not assigned!");
        }
    }

    public void AddItem(ItemName itemName)
    {
        if (!itemList.Contains(itemName))
        {
            itemList.Add(itemName);
            if (itemData != null)
            {
                EventHandler.CallUpdateUIEvent(itemData.GetItemDetails(itemName), itemList.Count - 1);
            }
            else
            {
                Debug.LogError("itemData is null, cannot call GetItemDetails!");
            }
        }
    }

    public int GetItemIndex(ItemName itemName)
    {
        for(int i = 0;i < itemList.Count;i++)
        {
            if (itemList[i] == itemName)
                return i;
        }
        return -1;
    }
}