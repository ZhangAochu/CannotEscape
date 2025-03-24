using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeController : MonoBehaviour
{
    public float flySpeed = 15f;
    private bool isFlying = false;
    private Rigidbody2D rb;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
        initialRotation = transform.rotation;

    }

    void Update()
    {
        // 自动回收检测
        if (isFlying && IsOutOfScreen())
        {
            ResetKnife();
        }
    }

    void OnMouseDown()
    {
        if (!isFlying)
        {
            LaunchKnife();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            KnifeThrowManager.Instance.HitMonster();
            ResetKnife();
            Destroy(collision.gameObject); // 销毁怪物
        }
    }

    void LaunchKnife()
    {
        isFlying = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.velocity = transform.up * flySpeed;
    }

    void ResetKnife()
    {
        isFlying = false;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;

        // 重置位置和旋转
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }

    bool IsOutOfScreen()
    {
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
        return viewportPos.y > 1.1f; // 超出屏幕上边界
    }
}
