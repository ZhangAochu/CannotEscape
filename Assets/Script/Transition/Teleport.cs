using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    public string sceneFrom;
    public string sceneTo;


    public virtual void TeleportToScene()
    {
        TransitionManager.Instance.Transition(sceneFrom, sceneTo);
    }

}
