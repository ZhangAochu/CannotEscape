using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doctor : Interactive
{
    private BoxCollider2D coll;
    private void Awake()
    {
        coll = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadEvent;
    }

    private void OnDisable()
    {
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadEvent;
    }

    private void OnAfterSceneLoadEvent()
    {
        if (!isDone)
        {
            // 场景加载后重置状态（如果需要）
            coll.enabled = true;
        }
        else
        {
            coll.enabled = false;
        }
    }

    protected override void OnClickedAction()
    {
        //给予病历
        InventoryManager.Instance.AddItem(rewardItem);

        // 关闭碰撞体
        coll.enabled = false;

        // 标记为已完成
        isDone = true;
    }
}
