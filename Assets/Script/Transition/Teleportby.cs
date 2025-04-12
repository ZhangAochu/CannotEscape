using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportby : Teleport
{

    [Header("Item Requirements")]
    [Tooltip("需要持有的物品ID")]
    public ItemName requiredItem; // 需要在物品栏中持有的物品ID
    public bool isDone;

    // 重写TeleportToScene方法，添加物品检查
    public void TeleportToSceneby(ItemName itemName)
    {
        // 检查物品栏中是否有需要的物品
        if (itemName == requiredItem && !isDone)
        {
            isDone = true;
            // 调用父类的传送方法
            base.TeleportToScene();
        }
    }
}
