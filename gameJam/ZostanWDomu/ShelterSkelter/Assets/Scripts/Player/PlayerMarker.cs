using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMarker : MonoBehaviour
{
    public Sprite[] player;

    public SpriteRenderer[] r;


    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<4; i++)
        {
            r[i].sprite = player[i];
        }
    }

    public void OnPlayerEnter(int i)
    {
        r[i].enabled = true;
        Debug.Log(i);
    }
    public void OnPlayerExit(int i)
    {
        r[i].enabled = false;
    }
}
