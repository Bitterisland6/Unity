using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class DragItem : MonoBehaviour
{
    EventSystem eventSystem;
    bool drag = false;
    public Slot currentSlot;
    public Item inHand = null;
    public Image icon;
    public Vector3 rectOffset;
    private GameObject player;
    private void Start()
    {
        eventSystem = GetComponent<EventSystem>();
        inHand = null;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        if(drag)
        {
            if(Input.GetMouseButtonDown(0))
            {
                EndDrag();
                CursorUpdate();
            }
            icon.rectTransform.position = Input.mousePosition + rectOffset;
        }
        else
        {
            if (EventSystem.current.IsPointerOverGameObject() && eventSystem.currentSelectedGameObject && Input.GetMouseButtonDown(0))
            {
                StartDrag();
                CursorUpdate();
            }
        }
    }
    private void CursorUpdate()
    {
        if (inHand == null)
        {
            icon.enabled = false;
        }
        else 
        {
            icon.enabled = true;
            icon.sprite = Resources.Load<Sprite>("Items/Icons/" + inHand.iconName);
        }
    }
    void StartDrag()
    {
        if(currentSlot != null)
        {
            drag = true;
            inHand = currentSlot.container.PickUpItem(currentSlot.x, currentSlot.y, inHand);
            currentSlot.CallUpdate();
        }
    }
    void EndDrag()
    {
        if(currentSlot == null)
        {
            DropItem();
            drag = false;
        }
        else
        {
            icon.sprite = currentSlot.icon.sprite;
            inHand = currentSlot.container.PickUpItem(currentSlot.x, currentSlot.y, inHand);
            currentSlot.CallUpdate();
            if (inHand == null)
            {
                drag = false;
            }
        }
    }
    private void DropItem()
    {
        if(inHand.itemType == "EqItem")
        {
            DroppedEqItem.Drop((EqItem)inHand, player.transform);
        }
        inHand = null;
    }
}
