using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnifeThrowManager : MonoBehaviour
{
    // 单例模式实例，允许全局访问
    public static KnifeThrowManager Instance;

    [Header("UI Settings")]
    public Slider healthSlider;        // 血条滑动条组件
    public GameObject victoryPanel;    // 胜利提示面板

    [Header("Game Settings")]
    public GameObject monsterPrefab;   // 怪物预制体
    public Transform spawnPoint; 
    public GameObject teleportToCorridor;
    public GameObject backButton;      // 怪物生成位置

    private int maxHealth = 3;         // 怪物总血量（分3次击破）
    private int currentHealth;         // 当前剩余血量
    

    // 在对象初始化时调用（早于Start）
    void Awake()
    {
        Instance = this;               // 建立单例引用
        InitializeGame();              // 初始化游戏状态
    }

    // 初始化游戏核心参数
    void InitializeGame()
    {
        currentHealth = maxHealth;                     // 重置血量
        healthSlider.maxValue = maxHealth;             // 设置血条最大值
        healthSlider.value = currentHealth;            // 同步当前血量显示
        SpawnMonster();                                // 生成第一个怪物
        victoryPanel.SetActive(false);                 // 隐藏胜利面板
        teleportToCorridor.SetActive(false);
        backButton.SetActive(false);
    }

    // 处理命中怪物事件
    public void HitMonster()
    {
        currentHealth--;                               // 扣除血量
        healthSlider.value = currentHealth;            // 更新血条显示

        if (currentHealth <= 0)
        {
            ShowVictory();                             // 血量归零显示胜利
            return;
        }
        SpawnMonster();                                // 生成新怪物
    }

    // 生成怪物实例
    void SpawnMonster()
    {
        // 在指定位置生成预制体，保持默认旋转
        Instantiate(monsterPrefab, spawnPoint.position, Quaternion.identity);
    }

    // 显示胜利界面
    void ShowVictory()
    {
        victoryPanel.SetActive(true);                  // 激活胜利面板
        Time.timeScale = 0;                            // 暂停游戏时间
        teleportToCorridor.SetActive(true);
        backButton.SetActive(true);
    }
}