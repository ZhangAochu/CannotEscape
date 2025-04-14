using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemHolderController : MonoBehaviour
{
    public string[] hideScenes; // 需要隐藏ItemHolder的场景名称
    public GameObject itemHolder; // 拖拽ItemHolder对象到这里

    void Awake()
    {
        // 确保父Canvas是持久化的
        DontDestroyOnLoad(transform.root.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (itemHolder != null)
        {
            bool shouldHide = System.Array.Exists(hideScenes, s => s == scene.name);
            itemHolder.SetActive(!shouldHide);
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}