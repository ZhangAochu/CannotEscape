using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    private Vector2 mouseWorldPos => Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));

    private bool canClick;

    private void Update()
    {
        canClick = ObjectAtMousePosition();

        if(canClick && Input.GetMouseButtonDown(0))
        {
            ClickAction(ObjectAtMousePosition().gameObject);
        }
    }

    private void ClickAction(GameObject clickObject)
    {
        switch(clickObject.tag)
        {
            case "Teleport":
                var teleport = clickObject.GetComponent<Teleport>();
                teleport?.TeleportToScene();
                break;
            case "Item":
                var item = clickObject.GetComponent<Item>();
                item?.ItemClicked();
                break;
        }
    }
    private Collider2D ObjectAtMousePosition()
    {
        return Physics2D.OverlapPoint(mouseWorldPos);
    }
}
