using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public Connection room;
    public int exitIndex;
    PlayerController pc;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            room.ExitRoom(exitIndex);
            pc = other.gameObject.GetComponentInParent<PlayerController>();
            //pc.ShakeScreen();        
        }
    }
}
