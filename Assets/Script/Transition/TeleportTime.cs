using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportTime : Teleport
{
    public void TeleportToSceneTime()
    {
        // 恢复时间尺度
        Time.timeScale = 1;
        Debug.Log($"Time.timeScale set to {Time.timeScale} before base.TeleportToScene().");

        // 检查TransitionManager实例是否为空
        if (TransitionManager.Instance != null)
        {
            base.TeleportToScene();
            Debug.Log($"子类");
        }
        else
        {
            Debug.LogError("TransitionManager 实例为空，无法进行场景切换。");
        }
    }

    public override void TeleportToScene()
    {
        // 恢复时间尺度
        Time.timeScale = 1;
        Debug.Log($"Time.timeScale set to {Time.timeScale} in TeleportToScene override method.");

        base.TeleportToScene();
        Debug.Log($"在重写的TeleportToScene方法中执行完父类的TeleportToScene方法");
    }
}