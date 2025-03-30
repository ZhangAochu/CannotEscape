using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTooltip : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }

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
