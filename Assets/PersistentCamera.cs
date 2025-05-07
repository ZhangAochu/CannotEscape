using UnityEngine;

public class PersistentCamera : MonoBehaviour
{
    private static PersistentCamera _instance;
    public float targetAspect = 16f / 9f; // 目标宽高比

    void Start()
    {
        // 获取当前屏幕的宽高比
        float currentAspect = (float)Screen.width / Screen.height;

        // 计算需要调整的比例
        if (currentAspect > targetAspect)
        {
            Camera.main.orthographicSize /= currentAspect / targetAspect;
        }
    }
    void Awake()
    {
        // 单例模式保证只有一个摄像机存在
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void OnEnable()
    {
        // 确保每次场景加载后更新摄像机状态
        Camera.main.enabled = true;
    }
}
