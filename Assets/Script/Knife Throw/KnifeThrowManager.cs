using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnifeThrowManager : MonoBehaviour
{
    public static KnifeThrowManager Instance;

    public Slider healthSlider;
    public GameObject monsterPrefab;
    public Transform spawnPoint;
    public GameObject victoryPanel;

    private int maxHealth = 3;
    private int currentHealth;

    void Awake()
    {
        Instance = this;
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        SpawnMonster();
    }

    public void HitMonster()
    {
        currentHealth--;
        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            victoryPanel.SetActive(true);
            return;
        }
        SpawnMonster();
    }

    void SpawnMonster()
    {
        Instantiate(monsterPrefab, spawnPoint.position, Quaternion.identity);
    }
}
