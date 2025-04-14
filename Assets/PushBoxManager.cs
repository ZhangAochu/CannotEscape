using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBoxManager : MonoBehaviour
{
    public int totalBox;
    public int finishedBox;

    public GameObject victoryPanel;
    public GameObject teleport;
    public GameObject backButton;

    private void Start()
    {
        victoryPanel.SetActive(false);
        teleport.SetActive(false);
        backButton.SetActive(false);
    }
    public void CheckFinish()
    {
        if(finishedBox == totalBox)
        {
            victoryPanel.SetActive(true);
            teleport.SetActive(true);
            backButton.SetActive(true);
        }
    }
    

}
