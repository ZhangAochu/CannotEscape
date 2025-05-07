// GameController.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("游戏元素配置")]
    public GameObject btn;
    public GameObject btn_tele;
    public float intervalPosX = 2;
    public float intervalPosY = 1;
    public GameObject holeObj;
    public GameObject moleObj;
    public Timer timer;
    public float appearFrequancy = 2f;

    [Header("场景配置")]
    private const string BOSS_SCENE_NAME = "Lab203 Boss Fight";

    private Canvas bcanvas;
    private bool canIncreaseMole = true;

    public struct Hole
    {
        public bool isAppear;
        public int holeX;
        public int holeY;
        public GameObject mole;
    }

    public Hole[] holes;

    void Start()
    {
        DontDestroyOnLoad(gameObject); // 保持跨场景存在
        PreloadBossScene();
        InitMap();
        StartGame();
    }

    private void PreloadBossScene()
    {
        if (!SceneManager.GetSceneByName(BOSS_SCENE_NAME).isLoaded)
        {
            SceneManager.LoadScene(BOSS_SCENE_NAME, LoadSceneMode.Additive);
        }
    }

    private void InitMap()
    {
        bcanvas = GameObject.Find("Boss Canvas").GetComponent<Canvas>();
        Vector2 originalPos = new Vector2(-2, -2);
        holes = new Hole[9];

        Scene targetScene = SceneManager.GetSceneByName(BOSS_SCENE_NAME);

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                int index = i * 3 + j;
                holes[index] = new Hole
                {
                    holeX = (int)(originalPos.x + j * intervalPosX),
                    holeY = (int)(originalPos.y + i * intervalPosY),
                    isAppear = false
                };

                GameObject hole = Instantiate(holeObj,
                    new Vector3(holes[index].holeX, holes[index].holeY, 0),
                    Quaternion.identity);

                MoveToScene(hole, targetScene);
            }
        }
    }

    private void StartGame()
    {
        MoleAppearFrequancy(appearFrequancy);
        timer.CountDown(true);
        btn.SetActive(false);
        btn_tele.SetActive(false);
    }

    private void MoleAppearFrequancy(float frequency)
    {
        CancelInvoke();
        InvokeRepeating(nameof(MoleAppear), 0f, frequency);
    }

    private void MoleAppear()
    {
        int id = GetAvailableHoleID();
        if (id == -1) return;

        Scene targetScene = SceneManager.GetSceneByName(BOSS_SCENE_NAME);
        Vector3 pos = new Vector3(holes[id].holeX, holes[id].holeY, 0);

        holes[id].mole = Instantiate(moleObj, pos, Quaternion.identity);
        MoveToScene(holes[id].mole, targetScene);

        Mole moleComponent = holes[id].mole.GetComponent<Mole>();
        moleComponent.id = id;
        moleComponent.SetGameController(this);

        holes[id].isAppear = true;
    }

    private int GetAvailableHoleID()
    {
        List<int> availableIDs = new List<int>();
        for (int i = 0; i < holes.Length; i++)
        {
            if (!holes[i].isAppear) availableIDs.Add(i);
        }
        return availableIDs.Count > 0 ? availableIDs[Random.Range(0, availableIDs.Count)] : -1;
    }

    public void CleanHoleState()
    {
        for (int i = 0; i < holes.Length; i++)
        {
            if (holes[i].mole == null) holes[i].isAppear = false;
        }
    }

    private void GameOver()
    {
        timer.CountDown(false);
        CancelInvoke();
        btn.SetActive(true);
        btn_tele.SetActive(true);
    }

    void Update()
    {
        CleanHoleState();

        if (timer.time < 15 && canIncreaseMole)
        {
            appearFrequancy = Mathf.Max(0.5f, appearFrequancy - 0.1f);
            MoleAppearFrequancy(appearFrequancy);
            canIncreaseMole = false;
        }

        if (timer.time < 0) GameOver();
    }

    private void MoveToScene(GameObject obj, Scene scene)
    {
        if (scene.IsValid() && obj.scene != scene)
        {
            SceneManager.MoveGameObjectToScene(obj, scene);
        }
    }
}