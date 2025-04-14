using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : Interactive
{
    protected override void OnClickedAction()
    {
        //µã»÷£¬Ïú»Ù³¡¾°
        this.gameObject.SetActive(false);

        isDone = true;
    }

}
