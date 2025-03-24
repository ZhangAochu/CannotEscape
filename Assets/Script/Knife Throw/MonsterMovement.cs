using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float leftBound = -5f;
    public float rightBound = 5f;
    private int direction = 1;

    void Update()
    {
        // 2DºáÏòÒÆ¶¯£¨XÖá£©
        transform.Translate(Vector2.right * direction * moveSpeed * Time.deltaTime);

        if (transform.position.x > rightBound)
            direction = -1;
        else if (transform.position.x < leftBound)
            direction = 1;
    }
}
