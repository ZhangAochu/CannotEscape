using UnityEngine;

public class ClickOnce : MonoBehaviour
{
    private BoxCollider2D col;

    // 使用静态字典实现运行时记忆（进程关闭自动重置）
    private static readonly System.Collections.Generic.Dictionary<string, bool> clickedStates =
        new System.Collections.Generic.Dictionary<string, bool>();

    private string objectID; // 唯一标识符

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();

        // 生成唯一ID：场景名+物体名+位置哈希（确保不同场景/位置的物体不会冲突）
        objectID = $"{gameObject.scene.name}_{gameObject.name}_{transform.position.GetHashCode()}";
    }

    private void Start()
    {
        // 检查是否已点击过（仅限当前游戏会话）
        if (clickedStates.TryGetValue(objectID, out bool isClicked) && isClicked)
        {
            DisablePermanently();
        }
    }

    private void OnMouseDown()
    {
        if (col.enabled)
        {
            StartCoroutine(DisableAfterClick());
            clickedStates[objectID] = true; // 记录点击状态
        }
    }

    private System.Collections.IEnumerator DisableAfterClick()
    {
        yield return new WaitForEndOfFrame();
        DisablePermanently();
    }

    private void DisablePermanently()
    {
        col.enabled = false;
        // 可选：添加视觉效果
        // GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f);
    }

    // 可选：场景切换时重置状态（如果需要）
    public static void ResetStatesForScene(string sceneName)
    {
        var keysToRemove = new System.Collections.Generic.List<string>();
        foreach (var key in clickedStates.Keys)
        {
            if (key.StartsWith(sceneName))
            {
                keysToRemove.Add(key);
            }
        }
        foreach (var key in keysToRemove)
        {
            clickedStates.Remove(key);
        }
    }
}