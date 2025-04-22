using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    public ItemDataList_SO itemData;
    public List<ItemName> itemList = new List<ItemName>();

    // 定义 4 个特定物品
    private List<ItemName> specificItems = new List<ItemName> { ItemName.MedicalRecord, ItemName.MedicalRecord_2, ItemName.MedicalRecord_3, ItemName.MedicalRecord_4 };
    // 定义新物品
    private ItemName newItem = ItemName.MedicalRecord_all;

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
        if (index >= 0 && index < itemList.Count)
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
            for (int i = 0; i < itemList.Count; i++)
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
            EventHandler.CallUpdateUIEvent(itemData.GetItemDetails(itemList[0]), 0);
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

            // 检查是否包含 4 个特定物品
            if (HasAllSpecificItems())
            {
                // 删除 4 个特定物品
                RemoveSpecificItems();

                // 给予新物品
                itemList.Add(newItem);
                if (itemData != null)
                {
                    EventHandler.CallUpdateUIEvent(itemData.GetItemDetails(newItem), itemList.Count - 1);
                }
                else
                {
                    Debug.LogError("itemData is null, cannot call GetItemDetails!");
                }
            }
            else
            {
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
    }

    private bool HasAllSpecificItems()
    {
        foreach (ItemName specificItem in specificItems)
        {
            if (!itemList.Contains(specificItem))
            {
                return false;
            }
        }
        return true;
    }

    private void RemoveSpecificItems()
    {
        foreach (ItemName specificItem in specificItems)
        {
            itemList.Remove(specificItem);
        }
    }

    public int GetItemIndex(ItemName itemName)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i] == itemName)
                return i;
        }
        return -1;
    }
}