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
            ItemName.Noodle => "Noodle",
            ItemName.Hammer => "Hammer",
            ItemName.MedicalRecord => "Record",
            ItemName.Power => "Power",
            ItemName.MedicalRecord_2 => "Record2",
            ItemName.MedicalRecord_3 => "Record3",
            ItemName.MedicalRecord_4 => "Record4",
            ItemName.MedicalRecord_all => "2468",
            _ => ""
        };
    }
}
