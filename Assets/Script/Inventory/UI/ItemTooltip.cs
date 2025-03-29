using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTooltip : MonoBehaviour
{
    public Text itemNameText;

    public void UpdateItemName(ItemName itemName)
    {
        itemNameText.text = itemName switch
        {
            ItemName.Remote => "Remote",
            _ => ""
        };
    }
}
