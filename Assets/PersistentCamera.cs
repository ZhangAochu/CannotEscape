using UnityEngine;

public class PersistentCamera : MonoBehaviour
{
    private static PersistentCamera _instance;

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
