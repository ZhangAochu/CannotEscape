using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportNeed : Teleport
{
    private static bool _can = false;
    public bool can
    {
        get { return _can; }
        set { _can = value; }
    }

    public DialogueData_SO FirstDialogue;//第一次

    public void TeleportToSceneDialogue()
    {
        Debug.Log("调用 TeleportToSceneDialogue 方法时，can 的值: " + can);
        if (_can)
        {
            DialogueController.Instance.ShowDialogueFromItem(FirstDialogue, () =>
            {
                base.TeleportToScene();
            });
        }
        else
        {
            // 可以在这里添加日志输出，确认进入了 else 分支
            Debug.Log("can 为 false，不进行场景跳转");
        }
    }
}