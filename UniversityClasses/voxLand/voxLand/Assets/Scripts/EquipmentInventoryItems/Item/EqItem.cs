using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Text;

[System.Serializable]
public class EqItem : Item
{
    public string modelName;
    public EqSlots slot;
    public List<ItemStat> stats;
    public Rarity rarity;
    public List<ItemStat> magicStats;
    string prefix;
    string sufix;
    string lore;
    public EqItem()
    {
        stats = new List<ItemStat>();
        magicStats = new List<ItemStat>();
    }
    public EqItem(XmlDocument xml) : base(xml)
    {
        modelName = xml["data"]["modelName"].InnerText;
        slot = (EqSlots)int.Parse(xml["data"]["eqSlot"].InnerText);
        stats = new List<ItemStat>();
        foreach (XmlElement elem in xml["data"]["stats"].ChildNodes)
        {
            stats.Add(new ItemStat(elem));
        }
        magicStats = new List<ItemStat>();
        RarityEval(xml);
        LoadStats(xml);
    }
    private void RarityEval(XmlDocument xml)
    {
        var rar = xml["data"]["rarity"];
        while((int)rarity < 6)
        {
            if (Random.Range(0, 100) <= int.Parse(rar[((Rarity)(rarity+1)).ToString("g")]["chance"].InnerText))
            {
                rarity++;
            }
            else
            {
                return;
            }
        }
    }
    private void LoadStats(XmlDocument xml)
    {
        string rarityName = rarity.ToString("g");
        var fixes = xml["data"]["rarity"][rarityName]["fixes"].ChildNodes;
        if(fixes.Count == 0)
        {
            return;
        }
        var fix = fixes.Item(Random.Range(0, fixes.Count - 1));
        this.prefix = fix["prefix"].InnerText;
        this.sufix = fix["sufix"].InnerText;

        foreach (XmlElement elem in fix["stats"].ChildNodes)
        {
            magicStats.Add(new ItemStat(elem));
        }
    }
    override public object Clone()
    {
        EqItem item = new EqItem();

        item.iconName = iconName;
        item.itemName = itemName;
        item.itemType = itemType;
        item.maxStack = maxStack;
        item.quantity = quantity;
        item.modelName = modelName;
        item.slot = slot;

        item.rarity = rarity;
        item.stats = new List<ItemStat>();
        foreach(ItemStat stat in this.stats)
        {
            item.stats.Add(stat);
        }
        item.magicStats = new List<ItemStat>();
        foreach (ItemStat stat in this.magicStats)
        {
            item.magicStats.Add(stat);
        }
        item.prefix = this.prefix;
        item.sufix = this.sufix;
        item.lore = this.lore;
        return item;
    }
    public void Apply(Attributes att)
    {
        foreach(ItemStat stat in stats)
        {
            stat.Apply(att);
        }
        foreach (ItemStat stat in magicStats)
        {
            stat.Apply(att);
        }
    }
    public void UnApply(Attributes att)
    {
        foreach (ItemStat stat in stats)
        {
            stat.UnApply(att);
        }
        foreach (ItemStat stat in magicStats)
        {
            stat.UnApply(att);
        }
    }
    override public string ToString()
    {
        StringBuilder str = new StringBuilder(prefix+" "+itemName+" "+sufix+ "\n");
        foreach (ItemStat stat in stats)
        {
            str.Append(stat.ToString());
        }
        foreach (ItemStat stat in magicStats)
        {
            str.Append(stat.ToString());
        }
        return str.ToString();
    }
}

public enum Rarity
{
    common,
    uncommon,
    magic,
    rare,
    set,
    unique,
    legendary
}
