using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedEqItem : Interactable
{
    public EqItem item;
    EquipmentVisual visual;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Initiate(EqItem item)
    {
        visual = GetComponent<EquipmentVisual>();
        this.item = item;
        visual.Equip(item);
    }
    public static void Drop(EqItem item, Transform location)
    {
        GameObject dropObject = Resources.Load("Items/DroppedItem") as GameObject;
        GameObject obj = Instantiate(dropObject, location.position + 0.5f * Vector3.up, location.rotation);
        obj.GetComponent<DroppedEqItem>().Initiate(item);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            CanInteract i = other.GetComponent<CanInteract>();
            if(!i.interactables.Contains(this))
            {
                i.interactables.Add(this);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<CanInteract>().interactables.Remove(this);
        }
    }

    public override bool Action(GameObject agent)
    {
        agent.GetComponentsInChildren<ItemContainer>()?[1].PutIn(item);
        agent.GetComponent<CanInteract>().interactables.Remove(this);
        Destroy(gameObject);
        return false;
    }
}
