using UnityEngine;
using UnityEngine.SceneManagement;

public class IsVisible : MonoBehaviour
{
    public string[] hideScenes; // 需要隐藏的场景名称
    public GameObject objectToHide; // 拖拽需要隐藏的对象到这里

    void Awake()
    {
        // 确保父Canvas是持久化的
        DontDestroyOnLoad(transform.root.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (objectToHide != null)
        {
            bool shouldHide = System.Array.Exists(hideScenes, s => s == scene.name);
            objectToHide.SetActive(!shouldHide);
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}