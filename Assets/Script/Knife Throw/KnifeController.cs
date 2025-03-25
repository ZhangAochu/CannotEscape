using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float flySpeed = 15f;

    private Rigidbody2D rb;
    private Vector3 initialPosition;
    private bool isActive = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
        SetPhysicsActive(false);
    }

    void Update()
    {
        CheckBoundary();
    }

    void OnMouseDown()
    {
        if (isActive) Launch();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        bool hitMonster = false;

        if (other.CompareTag("Monster"))
        {
            HandleMonsterCollision(other.gameObject);
            hitMonster = true;
            Debug.Log("命中怪物，开始重置");
            ResetKnife();
        }

        // 只有未击中怪物时重置
        if (!hitMonster) {
            Debug.Log("未命中怪物，开始重置");
            ResetKnife();
        }
    }

    void Launch()
    {
        isActive = false;
        SetPhysicsActive(true);
        rb.velocity = transform.up * flySpeed;
    }

    void ResetKnife()
    {
        SetPhysicsActive(false);
        transform.position = initialPosition;
        isActive = true;
    }

    void CheckBoundary()
    {
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
        if (viewportPos.y > 1.1f) ResetKnife();
    }

    void SetPhysicsActive(bool state)
    {
        rb.bodyType = state ? RigidbodyType2D.Dynamic : RigidbodyType2D.Kinematic;
        rb.velocity = Vector2.zero;
    }

    void HandleMonsterCollision(GameObject monster)
    {
        KnifeThrowManager.Instance.HitMonster();
        Destroy(monster);
    }
}
