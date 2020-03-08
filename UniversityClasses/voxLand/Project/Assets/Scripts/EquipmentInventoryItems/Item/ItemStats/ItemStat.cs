using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
[System.Serializable]
public class ItemStat
{
    public ItemStat(string name, AttType type, int value, int minValue, int maxValue)
    {
        this.attName = name;
        this.type = type;
        this.value = value;
        this.minValue = minValue;
        this.maxValue = maxValue;
    }
    public ItemStat(XmlElement xml)
    {
        this.attName = xml["name"].InnerText;
        this.type = (AttType)int.Parse(xml["type"].InnerText);
        this.minValue = int.Parse(xml["minValue"].InnerText);
        this.maxValue = int.Parse(xml["maxValue"].InnerText);
        this.value = Random.Range(minValue, maxValue);
    }
    string attName;
    AttType type;
    int value;
    int minValue;
    int maxValue;
    public ItemStat Clone()
    {
        return new ItemStat(attName, type, value, minValue, maxValue);
    }
    public void Apply(Attributes att)
    {
        switch(type)
        {
            case AttType.raw:
                att.GetStat(attName).Raw += value;
                break;
            case AttType.bonus:
                att.GetStat(attName).Bonus += value;
                break;
            case AttType.proc:
                att.GetStat(attName).Proc += value;
                break;
        }
    }
    public void UnApply(Attributes att)
    {
        switch (type)
        {
            case AttType.raw:
                att.GetStat(attName).Raw -= value;
                break;
            case AttType.bonus:
                att.GetStat(attName).Bonus -= value;
                break;
            case AttType.proc:
                att.GetStat(attName).Proc -= value;
                break;
        }
    }
    public override string ToString()
    {
        switch (type)
        {
            case AttType.raw:
                return $"+ {value} {attName}\n";
            case AttType.bonus:
                return $"++ {value} {attName}\n";
            case AttType.proc:
                return $"+ {value}% {attName}\n";
        }
        return "";
    }
    public string ToStringExt()
    {
        switch (type)
        {
            case AttType.raw:
                return $"+ {value}[{minValue}-{maxValue}] {attName}\n";
            case AttType.bonus:
                return $"++ {value}[{minValue}-{maxValue}] {attName}\n";
            case AttType.proc:
                return $"+ {value}[{minValue}-{maxValue}]% {attName}\n";
        }
        return "";
    }
}
public enum AttType
{
    proc,
    raw,
    bonus
}
