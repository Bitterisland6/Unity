using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentVisual : MonoBehaviour
{
    [System.Serializable]
    public class VisualSlot
    {
        public string[] suffixes;
        public Transform[] slotTransforms;
        public GameObject[] models;
        
    }
    [SerializeField]
    public VisualSlot[] slots;
    
    public void Equip(EqItem item)
    {
        VisualSlot slot = slots[(int)item.slot];
        for(int i = 0; i < slot.suffixes.Length; i++)
        {
            Destroy(slot.models[i]);
            GameObject model = Resources.Load("Items/Models/" + item.modelName + slot.suffixes[i]) as GameObject;
            if (model != null)
            {
                slot.models[i] = Instantiate(model, slot.slotTransforms[i]);
            }
        }
    }
    public void UnEquip(EqSlots slotNo)
    {
        VisualSlot slot = slots[(int)slotNo];
        for (int i = 0; i < slot.suffixes.Length; i++)
        {
            Destroy(slot.models[i]);
        }
    }
}
public enum EqSlots
{
    body,
    weapon,
    offHand,
    belt,
    helm,
    bracers,
    shoulders,
    gloves,
    boots,
    pants
}