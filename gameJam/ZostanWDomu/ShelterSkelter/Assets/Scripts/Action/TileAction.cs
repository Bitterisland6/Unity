using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "TileAction", menuName = "ScriptableObjects/TileAction", order = 3)]
public class TileAction : ScriptableObject
{
    //meta
    public ActionType type;
    public Sprite icon;
    [TextArea]
    public string discription;
    [Tooltip("0 => no limits")]
    public int numberOfUses;
    //cost
    public int aPtsCost;
    public int attackCost;

    public ResourceAmount[] resourceCost;

    public Device requiredDevice;
    //reward
    public int attackReward;

    public ResourceAmount[] resourceReward;

    public Device rewardDevice;
}
