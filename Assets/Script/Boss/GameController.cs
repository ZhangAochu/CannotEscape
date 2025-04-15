using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //描述洞口的结构体
    public struct Hole
    {
        //洞口是否出现了地鼠
        public bool isAppear;
        //洞口的横坐标
        public int holeX;
        //洞口的纵坐标
        public int holeY;
        //该洞口出现的地鼠
        public GameObject mole;
    }

    //表示洞口的一维数组，保存所有洞口的信息
    public Hole[] holes;
    //两个洞口间的横向间隔
    public float intervalPosX = 2;
    //两个洞口间的纵向间隔
    public float intervalPosY = 1;
    //要实例化的洞口预制体
    public GameObject holeObj;
    //要实例化的地鼠预制体
    public GameObject moleObj;
    //计时器控件
    public Timer timer;
    //地鼠出现频率（间隔2s）
    public float appearFrequancy = 2f;
    //是否修改地鼠出现频率
    private bool canIncreaseMole = true;

    //初始化开始界面函数（初始化九个洞口位置）
    private void InitMap()
    {
        //左下角洞口的坐标
        Vector2 originalPos = new Vector2(-2,-2);
        //分配存储洞口信息的内存
        holes = new Hole[9];
        //初始化每个洞口的位置信息并实例化洞口对象（预制体）
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0;j < 3; j++)
            {
                holes[i*3+j] = new Hole();
                //计算每一个洞口的横坐标
                holes[i*3+j].holeX = (int)(originalPos.x + j*intervalPosX);
                //计算每一个洞口的纵坐标
                holes[i*3+j].holeY = (int)(originalPos.y + i*intervalPosY);
                //表示当前洞口没有地鼠
                holes[i*3+j].isAppear = false;
                //实例化洞口对象
                Instantiate(holeObj,new Vector3(holes[i*3+j].holeX,holes[i*3+j].holeY,0),Quaternion.identity);
            }
        }
    }

    //地鼠出现的频率
    private void MoleAppearFrequancy(float appearFrequancy)
    {
        //停止产生地鼠
        CancelInvoke();
        //立即以appearFrequancy的频率重新开始产生地鼠
        InvokeRepeating("MoleAppear",0f,appearFrequancy);
    }

    // Start is called before the first frame update
    void Start()
    {
        //初始化洞口
        InitMap();
        //地鼠以0.7s的间隔出现
        MoleAppearFrequancy(appearFrequancy);
        //InvokeRepeating("MoleAppear",0f,0.5f);
        timer.CountDown(true);
    }

    //随机生成地鼠
    private void MoleAppear()
    {
        //获得随机数
        int id = UnityEngine.Random.Range(0,9);
        //判断当前洞口是否已经有地鼠了，为了放置死机，需要在Mole类中写Destroy函数
        while(holes[id].isAppear == true)
        {
            //如果当前洞口有随机数，则重新获得随机数
            id = UnityEngine.Random.Range(0,9);
        }
        //在对应id的洞口实例化地鼠对象
        holes[id].mole = Instantiate(moleObj,new Vector3(holes[id].holeX,holes[id].holeY,0),Quaternion.identity);
        //将随机生成的地鼠的id与洞口的id匹配
        holes[id].mole.GetComponent<Mole>().id = id;
        //将对应id洞口的isAppear置为true
        holes[id].isAppear = true;
        //Debug.Log("MoleAppear");
    }

    //清除洞口地鼠信息
    public void CleanHoleState()
    {
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0;j < 3; j++)
            {
                if(holes[i*3+j].mole == null)
                {
                    holes[i*3+j].isAppear = false;
                }
            }
        }
    }

    private void GameOver()
    {
        //不能再进行倒计时了，时间归0
        timer.CountDown(false);
        //将所有的InvokeRepeating()取消掉（停止生成地鼠）
        CancelInvoke();
    }

    // Update is called once per frame
    void Update()
    {
        //检测并清除isAppear
        CleanHoleState();
        //如果计时还剩15s，修改地鼠出现频率
        if(timer.time < 15 && canIncreaseMole == true)
        {
            //修改地鼠出现频率
            appearFrequancy -= 0.1f;
            //地鼠以appearFrequancy的频率出现
            MoleAppearFrequancy(appearFrequancy);
            //不能再修改地鼠出现频率
            canIncreaseMole = false;
        }
        //如果计时结束
        if(timer.time < 0)
        {
            //游戏结束提示
            GameOver();
        }
    }
}
