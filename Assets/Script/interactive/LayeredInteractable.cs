using UnityEngine;

public class LayeredInteractable : MonoBehaviour
{
    public Collider myCollider;
    public Collider coveredCollider; // 被覆盖的碰撞体
    public bool isActive = true; // 当前是否可交互

    private void Start()
    {
        // 初始化状态
        UpdateColliders();
    }

    public void OnInteracted()
    {
        // 当前物体被点击后的处理
        isActive = false;
        coveredCollider.GetComponent<LayeredInteractable>().Activate();
        UpdateColliders();

        // 这里可以添加你的自定义交互逻辑
        Debug.Log(gameObject.name + " 被点击");
    }

    public void Activate()
    {
        isActive = true;
        UpdateColliders();
    }

    private void UpdateColliders()
    {
        myCollider.enabled = isActive;
        coveredCollider.enabled = !isActive;
    }

    private void OnMouseDown()
    {
        if (isActive)
        {
            OnInteracted();
        }
    }
}