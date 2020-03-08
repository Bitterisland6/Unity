using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryAssetsActivation : MonoBehaviour
{
    public Connection room;
    public activationData[] toActivate;
    // Start is called before the first frame update
    void Start()
    {   
       int roomEntries = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().GetValue(room.uniqueRoomName);
        foreach(activationData d in toActivate)
        {
            if ((roomEntries <= d.end && roomEntries >= d.start))
            {
                d.whatToActivate.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
[System.Serializable]
public class activationData
{
    public GameObject whatToActivate;
    public int start;
    public int end;
}