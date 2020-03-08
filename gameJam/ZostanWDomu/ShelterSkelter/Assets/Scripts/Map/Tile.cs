using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tile : GameEventListener
{
    public TileAction tileAction;
    public TileAction moveAction;
    public bool shelter;
    private int timesUsed;
    [HideInInspector]
    public int playersOnTile;
    //non-global events
    public UnityEvent OnPlayerEnter;
    public UnityEvent OnPlayerExit;
    public UnityEvent OnActionSucces;
    public UnityEvent OnActionFailure;
    public UnityEvent OnNoMoreActions;

    public ResourceAmount[] dailyProduction;
    public ResourceAmount[] dailyCost;

    public Vector2Int pos;

    [SerializeField]
    Shader OutlineShader;
    
    Shader beginShader;
    Renderer renderer;
    bool clicked;
    public void ChangeTile(GameObject newTile)
    {
        GameObject nT;
        nT = Instantiate(newTile, transform.position, Quaternion.identity);
        TileMap.Instance().tileMap[pos.x, pos.y] = nT.GetComponent<Tile>();
        TileMap.Instance().tileMap[pos.x, pos.y].pos = this.pos;
        Destroy(gameObject);
    }
    private void Start()
    {
        action.AddListener(OnProductionPhase);
        OnPlayerEnter.AddListener(() => playersOnTile++);
        OnPlayerExit.AddListener(() => playersOnTile--);
        renderer = GetComponent<Renderer>();
        beginShader = renderer.material.shader;
        clicked = false;
    }

    private void OnProductionPhase()
    {
        bool costPaid = true;
        foreach(ResourceAmount r in dailyCost)
        {
            costPaid = Bank.Instance().Check(r);
        }
        if(costPaid)
        {
            foreach(ResourceAmount r in dailyCost)
            {
                Bank.Instance().Sub(r);
            }
            foreach (ResourceAmount r in dailyProduction)
            {
                Bank.Instance().Add(r);
            }
        }
    }
    //UI actions
    public void PerformAction(Player player, bool noRisk = false)
    {
        foreach(ResourceAmount r in tileAction.resourceCost)
        {
            Bank.Instance().Sub(r);
        }
        
        
        if (!noRisk)
        {
            RollDiceResult result = player.RollDice();
            //TODO DICE
            switch(result)
            {
                case RollDiceResult.fail:
                    OnActionFailure.Invoke();
                    player.ActionExecute(false, tileAction.type, tileAction.aPtsCost + (noRisk ? 1 : 0), pos);
                    return;
                case RollDiceResult.succes:
                    break;
                case RollDiceResult.dmg:
                    player.OnDamaged(1);
                    OnActionFailure.Invoke();
                    player.ActionExecute(false, tileAction.type, tileAction.aPtsCost + (noRisk ? 1 : 0), pos);
                    return;
                case RollDiceResult.dmgSucces:
                    player.OnDamaged(1);
                    break;
                case RollDiceResult.randomEvent:
                    RandomEventManager.Instance().InvokeRandomEvent(tileAction.type);
                    OnActionFailure.Invoke();
                    player.ActionExecute(false, tileAction.type, tileAction.aPtsCost + (noRisk ? 1 : 0), pos);
                    return;
                case RollDiceResult.randomEventSucces:
                    RandomEventManager.Instance().InvokeRandomEvent(tileAction.type);
                    break;
                case RollDiceResult.randomEventDmgFail:
                    RandomEventManager.Instance().InvokeRandomEvent(tileAction.type);
                    player.OnDamaged(1);
                    OnActionFailure.Invoke();
                    player.ActionExecute(false, tileAction.type, tileAction.aPtsCost + (noRisk ? 1 : 0), pos);
                    return;
                case RollDiceResult.randomEventDmgSucces:
                    RandomEventManager.Instance().InvokeRandomEvent(tileAction.type);
                    break;
            }
        }
        //reward
        foreach (ResourceAmount r in tileAction.resourceReward)
        {
            Bank.Instance().Add(r);
        }
        if (tileAction.rewardDevice != null)
        {
            Bank.Instance().devices.Add(tileAction.rewardDevice);
        }
        timesUsed++;
        if(tileAction.numberOfUses <= timesUsed)
        {
            OnNoMoreActions.Invoke();
        }
        OnActionSucces.Invoke();
        player.ActionExecute(true, tileAction.type, tileAction.aPtsCost + (noRisk ? 1 : 0), pos);
    }
    public void MoveHere(Player player, bool noRisk = false)
    {
        player.ActionExecute(true, moveAction.type, moveAction.aPtsCost + (noRisk ? 1 : 0), pos);
        foreach (ResourceAmount r in moveAction.resourceCost)
        {
            Bank.Instance().Sub(r);
        }


        if (!noRisk)
        {
            RollDiceResult result = player.RollDice();
            //TODO DICE
            switch (result)
            {
                case RollDiceResult.fail:
                    return;
                case RollDiceResult.succes:
                    break;
                case RollDiceResult.dmg:
                    player.OnDamaged(1);
                    return;
                case RollDiceResult.dmgSucces:
                    player.OnDamaged(1);
                    break;
                case RollDiceResult.randomEvent:
                    RandomEventManager.Instance().InvokeRandomEvent(moveAction.type);
                    return;
                case RollDiceResult.randomEventSucces:
                    RandomEventManager.Instance().InvokeRandomEvent(moveAction.type);
                    break;
                case RollDiceResult.randomEventDmgFail:
                    RandomEventManager.Instance().InvokeRandomEvent(moveAction.type);
                    player.OnDamaged(1);
                    return;
                case RollDiceResult.randomEventDmgSucces:
                    RandomEventManager.Instance().InvokeRandomEvent(moveAction.type);
                    break;
            }
        }
        //reward
        foreach (ResourceAmount r in moveAction.resourceReward)
        {
            Bank.Instance().Add(r);
        }
        if(moveAction.rewardDevice != null)
        {
            Bank.Instance().devices.Add(moveAction.rewardDevice);
        }
    }
    //UI helper
    public bool CanPerformAction(Player player, bool noRisk = false)
    {
        if(tileAction == null)
        {
            return false;
        }
        if (tileAction.type == ActionType.fight)
        {
            if (player.InRange(pos))
            {
                if (tileAction.aPtsCost + (noRisk ? 1 : 0) <= player.ActionPts(tileAction.type))
                {
                    return true;
                }
                return false;
            }
        }
        if (!(player.pos.x == pos.x && player.pos.y == pos.y))
        {
            return false;
        }
        if (tileAction.numberOfUses != 0 && tileAction.numberOfUses <= timesUsed)
        {
            return false;
        }
        if(tileAction.aPtsCost > player.ActionPts(tileAction.type))
        {
            return false;
        }
        if (noRisk && tileAction.aPtsCost + 1 > player.ActionPts(tileAction.type))
        {
            return false;
        }
        foreach (ResourceAmount r in tileAction.resourceCost)
        {
            if (!Bank.Instance().Check(r))
            {
                return false;
            }
        }
        if(tileAction.attackCost > player.attack)
        {
            return false;
        }  
        if(tileAction.requiredDevice != null && !Bank.Instance().devices.Contains(tileAction.requiredDevice))
        {
            return false;
        }
        return true;
    }
    public bool CanMoveHere(Player player, bool noRisk = false)
    {
        if(moveAction == null)
        {
            return false;
        }
        if(!player.InRange(pos))
        {
            return false;
        }
        if (moveAction.aPtsCost > player.ActionPts(moveAction.type))
        {
            return false;
        }
        if (noRisk && moveAction.aPtsCost + 1 > player.ActionPts(moveAction.type))
        {
            return false;
        }
        foreach (ResourceAmount r in moveAction.resourceCost)
        {
            if (!Bank.Instance().Check(r))
            {
                return false;
            }
        }
        if (moveAction.attackCost > player.attack)
        {
            return false;
        }
        if (moveAction.requiredDevice != null && !Bank.Instance().devices.Contains(moveAction.requiredDevice))
        {
            return false;
        }
        return true;
    }
    void OnMouseEnter() {
        if(!clicked)
            ChangeShader(OutlineShader);
    }

    void OnMouseExit() {
        if(!clicked)
            ResetShader();
    }

    void OnMouseDown() {  
        if(SelectedTile.Instance().selected != this) {
            SelectedTile.Instance().CancelSelection();
            SelectedTile.Instance().selected = this;
            SelectedTile.Instance().Selection();
            clicked = true;
        }
        else if(clicked) {
            clicked = false;
            SelectedTile.Instance().CancelSelection();
            SelectedTile.Instance().selected = null;
        }
        else
        {
            clicked = true;
            SelectedTile.Instance().selected = this;
            SelectedTile.Instance().Selection();
        }

        
         
    }

    public void ResetShader() {
        renderer.material.shader = beginShader;
        clicked = false;
    }

    public void ChangeShader(Shader newShader) {
        renderer.material.shader = newShader;
    }

}

