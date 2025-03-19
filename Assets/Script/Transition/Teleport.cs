using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public string sceneFrom;
    public string sceneTo;

    public void TeleportToScene()
    {
        TransitionManager.Instance.Transition(sceneFrom, sceneTo);
    }
   
}
