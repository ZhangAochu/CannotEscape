// Mole.cs
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mole : MonoBehaviour
{
    private const string BOSS_SCENE_NAME = "Lab203 Boss Fight";

    [Header("组件引用")]
    public Text scoreText;
    public GameObject beatenMole;

    [Header("游戏参数")]
    public int id;
    public static int score = 0;

    private GameController gameController;

    void Start()
    {
        EnsureInCorrectScene();
        SetupReferences();
        Destroy(gameObject, 2f);
    }

    private void EnsureInCorrectScene()
    {
        Scene targetScene = SceneManager.GetSceneByName(BOSS_SCENE_NAME);
        if (!targetScene.IsValid()) return;

        if (gameObject.scene != targetScene)
        {
            SceneManager.MoveGameObjectToScene(gameObject, targetScene);
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                targetScene.GetRootGameObjects()[0].transform.position.z
            );
        }
    }

    private void SetupReferences()
    {
        if (scoreText == null)
        {
            scoreText = GameObject.Find("ScoreText")?.GetComponent<Text>();
        }

        if (gameController == null)
        {
            gameController = FindObjectOfType<GameController>();
        }
    }

    public void SetGameController(GameController controller)
    {
        gameController = controller;
    }

    void OnMouseDown()
    {
        if (gameController == null) return;

        score++;
        UpdateScoreDisplay();
        CreateBeatenMole();
        UpdateGameController();
        DestroySelf();
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {score}";
        }
    }

    private void CreateBeatenMole()
    {
        Scene targetScene = SceneManager.GetSceneByName(BOSS_SCENE_NAME);
        GameObject beaten = Instantiate(beatenMole, transform.position, Quaternion.identity);
        SceneManager.MoveGameObjectToScene(beaten, targetScene);
    }

    private void UpdateGameController()
    {
        if (gameController != null && id >= 0 && id < gameController.holes.Length)
        {
            gameController.holes[id].mole = null;
            gameController.holes[id].isAppear = false;
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}