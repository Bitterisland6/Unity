using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoralePhase : GameEventListener
{
    // Start is called before the first frame update
    void Start()
    {
        action.AddListener(MoraleUpdate);
    }

    // Update is called once per frame
    void MoraleUpdate()
    {
        foreach (Player p in PlayerGroup.Instance().players)
        {
            if(p.inGame)
            {
                if (p.ActionPts(ActionType.none) == 2 && TileMap.Instance().tileMap[p.pos.x, p.pos.y].shelter)
                {
                    p.OnHealed(1);
                    p.OnMoraleChange(Random.Range(0, 2));
                }
            }

        }
    }
}
