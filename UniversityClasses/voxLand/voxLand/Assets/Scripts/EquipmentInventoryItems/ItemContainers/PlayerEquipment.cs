using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
[System.Serializable]
public class PlayerEquipment : ItemContainer
{
    private EquipmentVisual visual;
    private Attributes att;
    private void Start()
    {   
        att = GetComponent<Attributes>();
        visual = GetComponent<EquipmentVisual>();
        ItemContainerData data = new ItemContainerData(this.name, null);
        if (!SaveSystem.ItemContainerLoad(data) || forceNewData)
        {
            items = new Item[sizeX, sizeY];
        }
        else
        {
            
            this.items = data.items;
            this.sizeX = data.items.GetLength(0);
            this.sizeY = data.items.GetLength(1);
            
            foreach (Item item in items)
            {
                EqItem eqItem = (EqItem)item;
                if(eqItem != null)
                {
                    visual.Equip(eqItem);
                }
            }
        }   
    }
    override public bool FitsIn(Item item, int x, int y)
    {
        if(item == null)
        {
            return true;
        }
        if(item.itemType.Equals("EqItem"))
        {
            EqItem eqItem = (EqItem)item;
            if (x == (int)eqItem.slot)
            {
                return true;
            }
        }
        return false;
    }
    override public void OnItemTaken(int x, int y, int quantity)
    {
        EqItem eqItem = (EqItem)items[x, y];
        eqItem.UnApply(att);
        visual.UnEquip((EqSlots)x);
        items[x, y].quantity -= quantity;
        if (items[x, y].quantity <= 0)
        {
            items[x, y] = null;
        }
    }
    override public void OnItemAdded(Item item, int x, int y, int quantity)
    {
        if (item.itemName == items[x, y].itemName)
        {
            items[x, y].quantity += quantity;
        }
    }
    override public void OnItemPlaced(Item item, int x, int y)
    {
        EqItem eqItem = (EqItem)item;
        if(item == null)
        {
            return;
        }
        eqItem.Apply(att);
        items[x, y] = item;
        visual.Equip(eqItem);
    }
}
