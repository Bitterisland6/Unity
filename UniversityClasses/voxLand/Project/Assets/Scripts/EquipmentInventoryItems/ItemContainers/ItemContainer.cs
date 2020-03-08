using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ItemContainer : MonoBehaviour
{
    public Item[,] items;
    public int sizeX;
    public int sizeY;
    public bool forceNewData = true;
    
    public Item PickUpItem(int x, int y, Item fromHand)
    {
        Item item = items[x, y];
        Debug.Log($"\nfromHand: {fromHand?.ToString()}");
        Debug.Log($"\nitem: {item?.ToString()}");
        if(FitsIn(fromHand, x, y))
        {
            //stack part
            int toStack = CanStack(item, fromHand);
            if (toStack > 0)
            {
                fromHand.quantity -= toStack;
                if (fromHand.quantity <= 0)
                {
                    fromHand = null;
                }
                OnItemAdded(item, x, y, toStack);
                return fromHand;
            }
            //empty slot part
            if(item == null)
            {
                OnItemPlaced(fromHand, x, y);
                return item;
            }
            //swap part
            //take
            OnItemTaken(x, y, 0);
            items[x, y] = null;
            //put
            OnItemPlaced(fromHand, x, y);
            return item;
        }
        return fromHand;
    }
    static int CanStack(Item itemTo, Item itemFrom)
    {
        if(itemTo == null || itemFrom == null)
        {
            return 0;
        }
        if(itemTo.itemName == itemFrom.itemName)
        {
            int toMaxStack = itemTo.maxStack - itemTo.quantity;
            return System.Math.Min(toMaxStack, itemFrom.quantity);
        }
        return 0;
    }
    
    public virtual bool FitsIn(Item item, int x, int y)
    {
        return true;
    }
    public virtual void OnItemTaken(int x, int y, int quantity)
    {
        items[x, y].quantity -= quantity;
        if(items[x,y].quantity <= 0)
        {
            items[x, y] = null;
        }
    }
    public virtual void OnItemAdded(Item item, int x, int y, int quantity)
    {
        if(item.itemName == items[x,y].itemName)
        {
            items[x, y].quantity += quantity;
        }
    }
    public virtual void OnItemPlaced(Item item, int x, int y)
    {
        items[x, y] = item;
    }
    private void Start()
    {
        
        items = new Item[sizeX, sizeY];
        if (forceNewData)
        {
            if (this.GetType() != typeof(PlayerEquipment))
            {
                DebugCode();
            }
        }
        else
        {
            ItemContainerData data = new ItemContainerData(this.name, null);
            SaveSystem.ItemContainerLoad(data);
            this.items = data.items;
            this.sizeX = data.items.GetLength(0);
            this.sizeY = data.items.GetLength(1);
        }
    }
    private void DebugCode()
    {
        items[0, 0] = Item.ItemFromXml("helm");
        items[1, 0] = Item.ItemFromXml("weapon");
        items[1, 1] = Item.ItemFromXml("weapon");
        items[1, 2] = Item.ItemFromXml("shield");
        items[2, 0] = Item.ItemFromXml("item");
        items[4, 0] = Item.ItemFromXml("item");
        items[5, 0] = Item.ItemFromXml("item");
        items[6, 0] = Item.ItemFromXml("item");

        Debug.Log("added debug items");
    }
    private void OnDestroy()
    {
        SaveSystem.ItemContainerSave(new ItemContainerData(this));
    }
    public void PutIn(Item item)
    {
        for (int x = 0; x < sizeX; x++)
        {
            for(int y = 0; y < sizeY; y++)
            {
                if(items[x,y] == null)
                {
                    items[x, y] = item;
                    return;
                }
            }
        }
    }
}
[System.Serializable]
public class ItemContainerData
{
    public string name;
    public Item[,] items;
    public ItemContainerData(string name, Item[,] items)
    {
        this.name = name;
        this.items = items;
    }
    public ItemContainerData(ItemContainer container)
    {
        this.name = container.name;
        this.items = container.items;
    }
}