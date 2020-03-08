using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection : MonoBehaviour
{
    public string uniqueRoomName;
    public ConnectionData[] connections;
    private GameObject[] rooms;
    public GameObject[] exits;
    public bool startRoom;
    public RoomEnterTimes counter;
    public Cabinet[] cabinets;
    public Collectible[] collectibles;
    
    // Start is called before the first frame update
    void Start()
    {
        rooms = new GameObject[connections.Length];
        if(startRoom)
        {
            EnterRoom();
        }
    }
    public void SpawnRooms()
    {
        for(int i = 0; i < connections.Length; i++)
        {
            rooms[i] = Instantiate(connections[i].asset, transform.position + connections[i].offset, Quaternion.identity);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void ExitRoom(int into)
    {
        for(int i=0; i<connections.Length; i++)
        {
            if(i != into)
            {
                Destroy(rooms[i]);
            }
            else
            {
                rooms[i].GetComponent<Connection>().EnterRoom();
            }
        }
        Destroy(gameObject);
    }
    public void EnterRoom()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<CheckPoint>().SavePoint();
        player.GetComponent<PlayerController>().AddRoom(uniqueRoomName);
        SpawnRooms();
        foreach(GameObject exit in exits)
        {
            exit.SetActive(true);
        }
        counter.UpdateCounter(uniqueRoomName);
        foreach(Cabinet c in cabinets)
        {
            c.UpdateCabinet();
        }
        foreach (Collectible c in collectibles)
        {
            c.UpdateCollectible();
        }
        
    }
}
[Serializable]
public class ConnectionData
{
    public GameObject asset;
    public Vector3 offset;
}
