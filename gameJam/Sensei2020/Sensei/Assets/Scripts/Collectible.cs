using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    //variables
    [SerializeField]
    string name;

    PlayerController pc;

    public void UpdateCollectible() {
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        if(pc.colected.ContainsKey(name)) {
            Destroy(gameObject);
        }
        
    }

    public string GetName() {
        return name;
    }
}
