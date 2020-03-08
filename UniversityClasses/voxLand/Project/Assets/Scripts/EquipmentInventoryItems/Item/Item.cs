using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;

[System.Serializable]
public class Item : ICloneable
{
    public string itemType;
    public string iconName;
    public string itemName;
    public int maxStack;
    public int quantity;
    public Item()
    {

    }
    public Item(XmlDocument xml)
    {
        itemType = xml["data"]["itemType"].InnerText;
        iconName = xml["data"]["iconName"].InnerText;
        itemName = xml["data"]["itemName"].InnerText;
        maxStack = int.Parse(xml["data"]["maxStack"].InnerText);
        quantity = int.Parse(xml["data"]["quantity"].InnerText);
    }
    public static Item ItemFromXml(string xmlName)
    {
        TextAsset txt = Resources.Load("Items/XML/" + xmlName) as TextAsset;
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(txt.text);
        string itemType = xml["data"]["itemType"].InnerText;
        if (itemType == "EqItem")
        {
            return new EqItem(xml);
        }
        return new Item(xml);
    }

    virtual public object Clone()
    {
        Item item = new Item();
        item.iconName = iconName;
        item.itemName = itemName;
        item.itemType = itemType;
        item.maxStack = maxStack;
        item.quantity = quantity;
        return item;
    }
    override public string ToString()
    {
        return $"{itemName}\n" + (maxStack > 1 ? $"{quantity}/{maxStack}\n" : "");
    }
}
