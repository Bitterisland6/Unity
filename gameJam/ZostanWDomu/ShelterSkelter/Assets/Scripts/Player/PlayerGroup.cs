using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroup : MonoBehaviour
{
    public Player[] players;
    static PlayerGroup instance;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }
    public static PlayerGroup Instance()
    {
        if(instance == null)
        {
            Debug.LogError("PlayerGroup doesn't exist");
        }
        return instance;
    }
    public Player CurrentPlayer()
    {
        foreach(Player p in players)
        {
            if(p.myTurn && p.inGame)
            {
                return p;
            }
        }
        return null;
    }
}
