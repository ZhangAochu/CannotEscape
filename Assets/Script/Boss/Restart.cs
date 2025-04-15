using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using Mole;

public class Restart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RestartGame()
    {
        //重新加载该主场景
        SceneManager.LoadScene("WhacAMole");
        //将分数置0
        Mole.score = 0;
    }
}
