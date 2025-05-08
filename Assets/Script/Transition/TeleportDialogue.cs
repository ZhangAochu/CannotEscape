using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportDialogue : Teleport
{
    private static bool isfirsttime = true;
    public DialogueData_SO FirstDialogue;//第一次
    public DialogueData_SO EmptyDialogue;
    public TeleportNeed can;
    public void TeleportToSceneDialogue()
    {
        if (isfirsttime)
        {
            isfirsttime = false;
            if (can != null) // 添加空引用检查
            {
                can.can = true;
                Debug.Log("can 属性的值已修改为: " + can.can);
            }
            else
            {
                Debug.LogError("can 引用为空，无法修改 can 属性的值。");
            }
            DialogueController.Instance.ShowDialogueFromItem(FirstDialogue,()=>
            {
                base.TeleportToScene();
            });
        }
        else
        {
            base.TeleportToScene();
        }
        
    }
}