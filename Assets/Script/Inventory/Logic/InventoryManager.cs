using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    public ItemDataList_SO itemData;
    [SerializeField] private List<ItemName> itemlist = new List<ItemName>();

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
        if (!itemlist.Contains(itemName))
        {
            itemlist.Add(itemName);
            if (itemData != null)
            {
                EventHandler.CallUpdateUIEvent(itemData.GetItemDetails(itemName), itemlist.Count - 1);
            }
            else
            {
                Debug.LogError("itemData is null, cannot call GetItemDetails!");
            }
        }
    }
}