using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : MonoBehaviour
{
    [TextArea]
    public string label;
    public abstract void ActionStart(GameObject initiator = null);
}
