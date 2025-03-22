using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    public ItemDataList_SO itemData;
    [SerializeField]private List<ItemName> itemlist = new List<ItemName>();
    public void AddItem(ItemName itemName)
    {
         if (!itemlist.Contains(itemName))
        {
            itemlist.Add(itemName);
            //TODO:ui∂‘”¶œ‘ æ
        }
    }
}
