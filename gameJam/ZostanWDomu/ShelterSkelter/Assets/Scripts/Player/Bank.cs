using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bank : MonoBehaviour
{
    public UnityEvent onResourceChanged;
    public UnityEvent onPointsChanged;
    private void Awake()
    {
        instance = this;
    }
    static Bank instance;
    public static Bank Instance()
    {
        if(instance == null)
        {
            Debug.Log("Missing bank");
        }
        return instance;
    }
    public int metal;
    public int bricks;
    public int wood;
    public int food;
    public int points;
    public HashSet<Device> devices;
    public void AddPoints(int points)
    {
        this.points += points;
        onPointsChanged.Invoke();
    }
    public void Add(ResourceAmount resource)
    {
        switch(resource.type)
        {
            case ResourceType.bricks:
                bricks += resource.amount;
                break;
            case ResourceType.metal:
                metal += resource.amount;
                break;
            case ResourceType.food:
                food += resource.amount;
                break;
            case ResourceType.wood:
                wood += resource.amount;
                break;
        }
        onResourceChanged.Invoke();
    }
    public bool Check(ResourceAmount resource)
    {
        switch (resource.type)
        {
            case ResourceType.bricks:
                if (resource.amount > this.bricks) return false;
                break;
            case ResourceType.metal:
                if (resource.amount > this.metal) return false;
                break;
            case ResourceType.food:
                if (resource.amount > this.food) return false;
                break;
            case ResourceType.wood:
                if (resource.amount > this.wood) return false;
                break;
        }
        return true;
    }
    public void Sub(ResourceAmount resource)
    {
        switch (resource.type)
        {
            case ResourceType.bricks:
                bricks -= resource.amount;
                break;
            case ResourceType.metal:
                metal -= resource.amount;
                break;
            case ResourceType.food:
                food -= resource.amount;
                break;
            case ResourceType.wood:
                wood -= resource.amount;
                break;
        }
        onResourceChanged.Invoke();
    }
}
