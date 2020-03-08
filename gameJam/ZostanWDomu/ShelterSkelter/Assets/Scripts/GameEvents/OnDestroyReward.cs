using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDestroyReward : MonoBehaviour
{
    public int points;
    private void OnDestroy()
    {
        Bank.Instance().AddPoints(points);
    }
}
