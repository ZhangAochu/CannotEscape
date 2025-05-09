using UnityEngine;

public class GlobalDoctorState : MonoBehaviour
{
    // 使用静态属性确保线程安全的延迟初始化
    private static GlobalDoctorState _instance;
    public static GlobalDoctorState Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GlobalDoctorState>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("GlobalDoctorState");
                    _instance = singletonObject.AddComponent<GlobalDoctorState>();
                    Debug.Log("[GlobalDoctorState] 创建新实例");
                }

                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    public bool doctorIsAwake = false;

    // 改为 public 以便外部访问
    public bool isInitialized = false;

    // 初始化事件
    public static event System.Action OnInitialized;

    private void Awake()
    {
        // 如果已经存在实例，则销毁当前实例
        if (_instance != null && _instance != this)
        {
            Debug.LogWarning("[GlobalDoctorState] 发现重复实例，销毁当前实例");
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        // 执行初始化逻辑
        Initialize();
    }

    private void Initialize()
    {
        // 任何需要的初始化代码
        isInitialized = true;

        // 触发初始化完成事件
        OnInitialized?.Invoke();
        Debug.Log("[GlobalDoctorState] 初始化完成");
    }

    // 提供安全的访问方法
    public static void SafeSetDoctorAwake(bool value)
    {
        if (Instance.isInitialized)
        {
            Instance.doctorIsAwake = value;
        }
        else
        {
            Debug.LogWarning("[GlobalDoctorState] 尚未初始化，延迟设置doctorIsAwake");

            // 注册事件，在初始化完成后设置值
            OnInitialized += () => Instance.doctorIsAwake = value;
        }
    }
}