using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatenMole : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //0.5s后销毁被打的地鼠
        Destroy(gameObject,0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
