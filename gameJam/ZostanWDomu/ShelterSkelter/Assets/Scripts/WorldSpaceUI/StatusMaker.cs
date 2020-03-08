using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusMaker : MonoBehaviour
{
    private Queue<Sprite> statusQueue;
    public GameObject status;
    private void Start()
    {
        statusQueue = new Queue<Sprite>();
        InvokeRepeating("MakeStatus", 0, 0.75f);
    }
    public void CallStatus(Sprite icon)
    {
        statusQueue.Enqueue(icon);
    }
    private void MakeStatus()
    {
        if (statusQueue.Count == 0) return;
        GameObject o = Instantiate(status, transform.position, Quaternion.identity);
        o.GetComponentInChildren<Image>().sprite = statusQueue.Dequeue();
    }
}
