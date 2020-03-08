using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attributes : MonoBehaviour
{
    private void Start()
    {
        stats = new Dictionary<string, Attribute>();
    }
    public Dictionary<string, Attribute> stats;
    public Attribute GetStat(string name)
    {
        try
        {
            return stats[name];
        }
        catch(Exception e)
        {
            stats.Add(name, new Attribute(name));
            return stats[name];
        }
    }
    public Attributes(AttributesData data)
    {
        stats = new Dictionary<string, Attribute>();
        foreach(Attribute att in data.stats)
        {
            this.stats.Add(att.name, att);
        }
    }
}
[System.Serializable]
public class Attribute
{
    public Attribute(string name)
    {
        this.name = name;
    }
    public string name;
    private int proc;
    private int raw;
    private int bonus;
    private float calculated;

    public int Proc
    {
        get => proc;
        set
        {
            proc = value;
            calculated = ((float)(raw * proc) / 100) + bonus;
        }
    }
    public int Raw
    {
        get => raw;
        set
        {
            raw = value;
            calculated = ((float)(raw * proc) / 100) + bonus;
        }
    }
    public int Bonus
    {
        get => bonus;
        set
        {
            bonus = value;
            calculated = ((float)(raw * proc) / 100) + bonus;
        }
    }
    public float Calculated
    {
        get => calculated;
    }
}
public class AttributesData
{
    public List<Attribute> stats;
    public AttributesData(Attributes source)
    {
        stats = new List<Attribute>();
        foreach(Attribute att in source.stats.Values)
        {
            stats.Add(att);
        }
    }
}