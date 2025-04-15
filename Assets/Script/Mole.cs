using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mole : MonoBehaviour
{
    //显示分数的Text控件
    public Text scoreText;
    //记录分数的变量
    public static int score = 0;
    //显示被打地鼠
    public GameObject beatenMole;
    //对应洞口的id
    public int id;
    public GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        //获得显示分数的Text控件
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        //找到具有GameController的对象
        gameController = GameController.FindObjectOfType<GameController>();
        //3s后，销毁该生成的地鼠
        Destroy(gameObject,2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //重写MonoBehaviour.OnMouseDown()
    void OnMouseDown() 
    {
        //打到地鼠，分数+1
        score++;
        //显示分数
        scoreText.text = "Score:" + score;
        //实例化一个被打地鼠的对象
        gameController.holes[id].mole = Instantiate(beatenMole,gameObject.transform.position,Quaternion.identity);
        //销毁正常的地鼠
        Destroy(gameObject);
        //Debug.Log("OnMouseDown");
    }
}
