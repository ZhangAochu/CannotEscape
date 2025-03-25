using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeCtrllerDebug : MonoBehaviour
{
    [Header("调试设置")]
    public bool showInConsole = true;
    public bool showOnScreen = true;
    public int maxDisplayCount = 5;

    private Queue<string> collisionLog = new Queue<string>();
    private GUIStyle guiStyle;

    void OnTriggerEnter2D(Collider2D other)
    {
        string logEntry = $"碰撞对象：{other.name} 标签：{other.tag}";

        // 控制台输出
        if (showInConsole)
        {
            Debug.Log(logEntry + $" 位置：{other.transform.position}");
        }

        // 屏幕显示记录
        if (showOnScreen)
        {
            collisionLog.Enqueue(logEntry);
            if (collisionLog.Count > maxDisplayCount)
            {
                collisionLog.Dequeue();
            }
        }
    }

    void OnGUI()
    {
        if (!showOnScreen) return;

        InitializeGUIStyle();

        GUILayout.BeginArea(new Rect(10, 10, 300, 200));
        GUILayout.Label("=== 碰撞检测记录 ===", guiStyle);

        foreach (string log in collisionLog)
        {
            GUILayout.Label(log, guiStyle);
        }

        GUILayout.EndArea();
    }

    void InitializeGUIStyle()
    {
        if (guiStyle == null)
        {
            guiStyle = new GUIStyle();
            guiStyle.fontSize = 20;
            guiStyle.normal.textColor = Color.yellow;
            guiStyle.alignment = TextAnchor.UpperLeft;
        }
    }
}