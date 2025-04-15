using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    //计时的Text控件
    public Text timerText;
    //游戏时长为30s
    public float time = 30.0f;
    //判断是否能开始计时
    private bool canCountDown = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //如果可以开始计时
        if(canCountDown == true)
        {
            //开始计时
            time -= Time.deltaTime;
            //动态显示倒计时，时间保留一个小数点
            timerText.text = "Time:" + time.ToString("F1");
            //Debug.Log(time);
        }
    }

    //计时
    public void CountDown(bool countDown)
    {
        this.canCountDown = countDown;
        //如果不能开始计时
        if(canCountDown == false)
        {
            //计时置0
            time = 0;
            //输出Game Over
            timerText.text = "Game Over";
        }
    }
}
