using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCanvasController : MonoBehaviour
{
    [SerializeField] private string[] hideScenes; // 在Inspector中设置需要隐藏的场景名

    private Canvas mainCanvas;

    void Awake()
    {
        mainCanvas = GetComponent<Canvas>();
        DontDestroyOnLoad(gameObject); // 确保Canvas持久化
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 检查当前场景是否需要隐藏Canvas
        bool shouldHide = System.Array.Exists(hideScenes, sceneName => sceneName == scene.name);
        mainCanvas.enabled = !shouldHide;

        // 或者使用gameObject.SetActive(!shouldHide);
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}