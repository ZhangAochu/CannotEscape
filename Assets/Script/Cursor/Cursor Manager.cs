using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public RectTransform hand;
    private Vector2 mouseWorldPos => Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));

    private bool canClick;

    private bool holdItem;

    private ItemName currentItem;

    /** 
     * 创建一个单例
     * 提供一个对currentItem的只读访问
     * @袁新坪
     */
    public ItemName CurrentItem
    {
        get { return currentItem; }
    }
    public static CursorManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //

    private void OnEnable()
    {
        EventHandler.ItemSelectedEvent += OnItemSelectedEvent;
        EventHandler.ItemUsedEvent += OnItemUsedEvent;
    }  

    private void OnDisable()
    {
        EventHandler.ItemSelectedEvent -= OnItemSelectedEvent;
        EventHandler.ItemUsedEvent -= OnItemUsedEvent;
    }

    private void OnItemUsedEvent(ItemName name)
    {
        currentItem = ItemName.None;
        holdItem = false;
        hand.gameObject.SetActive(false);
    }

    private void Update()
    {
        canClick = ObjectAtMousePosition();

        if(hand.gameObject.activeInHierarchy)
        {
            hand.position = Input.mousePosition;
        }

        if(canClick && Input.GetMouseButtonDown(0))
        {
            ClickAction(ObjectAtMousePosition().gameObject);
        }
    }

    private void OnItemSelectedEvent(ItemDetails itemDetails, bool isSelected)
    {
        holdItem = isSelected;
        if (isSelected)
        {
            currentItem = itemDetails.itemName;      
        }
        hand.gameObject.SetActive(holdItem);
    }
    private void ClickAction(GameObject clickObject)
    {
        switch(clickObject.tag)
        {
            case "Teleport":
                var teleport = clickObject.GetComponent<Teleport>();
                teleport?.TeleportToScene();
                break;
            case "Teleportby":
                var teleportby = clickObject.GetComponent<Teleportby>();
                /*修改teleportby?使其允许空点击
                 * @袁新坪
                 */
                teleportby?.TeleportToSceneby(currentItem);
                break;
                /*
                if (holdItem)
                    teleportby?.TeleportToSceneby(currentItem);
                break;
                */
            case "Item":
                var item = clickObject.GetComponent<Item>();
                item?.ItemClicked();
                break;
            case "Interactive":
                var interactive=clickObject.GetComponent<Interactive>();
                if (holdItem)
                    interactive?.CheckItem(currentItem);
                else
                    interactive?.EmptyClicked();
                break;
        }
    }
    private Collider2D ObjectAtMousePosition()
    {
        return Physics2D.OverlapPoint(mouseWorldPos);
    }
}
