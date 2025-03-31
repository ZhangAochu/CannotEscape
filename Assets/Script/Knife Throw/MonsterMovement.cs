using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    public float leftBound = -5f;
    public float rightBound = 5f;

    private int direction = 1;

    void Update()
    {
        MoveHorizontally();
        CheckBoundaries();
        SetSpeedUp();
    }

    void MoveHorizontally()
    {
        transform.Translate(Vector2.right * direction * moveSpeed * Time.deltaTime);
    }

    void CheckBoundaries()
    {
        if (transform.position.x > rightBound) direction = -1;
        if (transform.position.x < leftBound) direction = 1;
    }

    // µ÷ÊÔÏÔÊ¾ÒÆ¶¯·¶Î§
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(
            new Vector2(leftBound, transform.position.y),
            new Vector2(rightBound, transform.position.y)
        );
    }

    void SetSpeedUp()
    {
        if (moveSpeed > 0 && moveSpeed < 30)
        {
            moveSpeed = moveSpeed + (float)0.01;
        }
    }
}
