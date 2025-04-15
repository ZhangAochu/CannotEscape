using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public LayerMask detectLayer;
    public Color finsihColor;
    Color originColor;
 

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        originColor = GetComponent<SpriteRenderer>().color;
        FindObjectOfType<PushBoxManager>().totalBox++;
    }
    public bool CanMoveToDir(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + (Vector3)dir * 0.5f, dir, 0.5f, detectLayer);

        if (!hit)
        {
            transform.Translate(dir);
            return true;
        }

            return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Target"))
        {
            FindObjectOfType<PushBoxManager>().finishedBox++;
            FindObjectOfType<PushBoxManager>().CheckFinish();
            GetComponent<SpriteRenderer>().color = finsihColor;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Target"))
        {
            FindObjectOfType<PushBoxManager>().finishedBox--;
            GetComponent<SpriteRenderer>().color = originColor;
        }
    }
}
