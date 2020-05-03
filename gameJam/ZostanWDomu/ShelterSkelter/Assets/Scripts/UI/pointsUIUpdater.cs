using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointsUIUpdater : MonoBehaviour
{
    int currentPointsTarget;
    int currentPointsViewed;
    public float rate;
    public TMPro.TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        Bank.Instance().onPointsChanged.AddListener(PointsUpdate);
        InvokeRepeating("UpdateView", 0, rate);
    }

    // Update is called once per frame
    void PointsUpdate()
    {
        currentPointsTarget = Bank.Instance().points;
    }
    void UpdateView()
    {
        if(currentPointsTarget != currentPointsViewed)
        {
            if(currentPointsTarget < currentPointsViewed)
            {
                currentPointsViewed--;
            }
            else
            {
                currentPointsViewed++;
            }
            text.text = currentPointsViewed.ToString();
        }
    }
}
