using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string sceneName; // 填写第二个场景的名称

    void Start()
    {
        // 异步加载第二个场景（叠加模式）
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }
}
