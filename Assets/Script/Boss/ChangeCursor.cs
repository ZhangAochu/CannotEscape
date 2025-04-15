using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCursor : MonoBehaviour
{
    public Sprite normalCursor;
    public Sprite hitCursor;
    public Image hammerImage;

    // Start is called before the first frame update
    void Start()
    {
        //隐藏鼠标箭头光标
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //如果鼠标左键被按下
        if(Input.GetMouseButton(0))
        {
            //将鼠标换成击打的锤子
            hammerImage.sprite = hitCursor;
        }
        else
        {
            //否则是正常的锤子
            hammerImage.sprite = normalCursor;
        }
        //锤子跟着鼠标位置移动
        hammerImage.rectTransform.position = Input.mousePosition;
    }
}
