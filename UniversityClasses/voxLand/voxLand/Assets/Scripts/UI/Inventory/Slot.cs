using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public ItemContainer container;
    public int x;
    public int y;
    private DragItem dragger;
    [HideInInspector]
    public Image icon;
    private Discription disc;
    private float lastUpdate;
    // Start is called before the first frame update
    void Start()
    {
        disc = FindObjectOfType<Discription>();
        icon = GetComponentsInChildren<Image>(true)[1];
        dragger = FindObjectOfType<DragItem>();
        InvokeRepeating("CallUpdate", 0.1f, 1.5f);
    }

    // Update is called once per frame
    public void CallUpdate()
    {
        if(gameObject.activeInHierarchy)
        {
            if (container.items[x, y] == null || container.items[x, y].itemName == "")
            {
                icon.enabled = false;
            }
            else
            {
                icon.sprite = Resources.Load<Sprite>("Items/Icons/" + container.items[x, y].iconName);
                icon.enabled = true;
            }
        }
        
    }
    public void MouseEnter()
    {
        dragger.currentSlot = this;
        if(container.items[x, y] != null)
        {
            disc.SetActive(container.items[x,y].ToString());
        }
    }
    public void MouseExit()
    {
        dragger.currentSlot = null;
        disc.DeActivate();
    }
}
