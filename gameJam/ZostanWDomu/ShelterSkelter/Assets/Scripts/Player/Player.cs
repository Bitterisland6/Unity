using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : GameEventListener
{
    public Sprite sprite;
    public bool inGame; //is this player playing
    public Hero hero;
    public GameEvent endOfTurn;
    public int aPtsMax;
    private int aPts;
    public int specialAPtsMax;
    private int sApts;
    public int hpMax;
    private int hp;
    public int startMorale;
    private int morale;
    public int attack;
    public bool myTurn;
    public float markerHeight;
    public AnimationCurve markerSpeed;
    [HideInInspector]
    public Vector2Int pos;
    

    [Header("non-global events")]
    //action
    public UnityEvent onMove;
    public UnityEvent onFight;
    public UnityEvent onRepair;
    public UnityEvent onDemolish;
    public UnityEvent onGatherFood;
    public UnityEvent onInvent;
    public UnityEvent onAction;
    public UnityEvent onActionFailed;
    //other
    public UnityEvent onDamaged;
    public UnityEvent onDeath;
    public UnityEvent onHealed;
    public UnityEvent onMoraleUp;
    public UnityEvent onMoraleDown;
    private void Start()
    {
        action.AddListener(OnTurnStart);
        hp = hpMax;
        morale = startMorale;
        if(inGame)InvokeRepeating("MovePlayer", 0.5f, 0.02f);
    }
    //for UI Actions

    //for message calls
    public void OnMoraleChange(int m)
    {
        if(m > 0)
        {
            if(morale < 1)
            {
                morale += m;
                morale = Mathf.Clamp(morale, -1, 1);
                onMoraleUp.Invoke();
            }
        }
        if(m < 0)
        {
            if (morale > -1)
            {
                morale += m;
                morale = Mathf.Clamp(morale, -1, 1);
                onMoraleDown.Invoke();
            }
        }
    }
    public void OnDamaged(int dmg)
    {
        hp -= dmg;
        onDamaged.Invoke();
        if(hp <= 0)
        {
            onDeath.Invoke();
        }
    }
    public void OnHealed(int heal)
    {
        if(hp<hpMax)
        {
            hp += heal;
            onHealed.Invoke();
        }
    }
    public void OnTurnStart()
    {
        aPts = aPtsMax;
        sApts = specialAPtsMax;
        onAction.Invoke();
    }
    //for UI helper Display
    public bool InRange(Vector2Int target)
    {
        if(target.x == pos.x && target.y == pos.y)
        {
            return false;
        }
        if (Mathf.Abs(target.x - pos.x) <= 1 && Mathf.Abs(target.y - pos.y) <= 1)
        {
            return true;
        }
        return false;
    }
    public int ActionPts(ActionType type)
    {
        switch (type)
        {
            case ActionType.move:
                return MoveActionPts();
            case ActionType.fight:
                return FightActionPts();
            case ActionType.repair:
                return RepairActionPts();
            case ActionType.demolition:
                return DemolitionActionPts();
            case ActionType.getFood:
                return GetFoodActionPts();
            case ActionType.invent:
                return InventActionPts();
            case ActionType.none:
                return aPts;
            default:
                return aPts;
        }
    }
    public void ActionExecute(bool succes, ActionType type, int cost, Vector2Int target = new Vector2Int())
    {
        switch(type)
        {
            case ActionType.move:
                if(hero == Hero.adventurer && sApts > 0)
                {
                    cost -= sApts;
                    sApts--;
                }
                aPts -= cost;
                pos = target;
                onMove.Invoke();
                onAction.Invoke();
                TileMap.Instance().PlayerMoved(target.x, target.y);
                Tile tileOut = TileMap.Instance().tileMap[pos.x, pos.y];
                Tile tileIn = TileMap.Instance().tileMap[target.x, target.y];
                tileOut.OnPlayerExit.Invoke();
                tileIn.OnPlayerEnter.Invoke();
                CancelInvoke("MovePlayer");
                InvokeRepeating("MovePlayer", 0, 0.02f);
                break;
            case ActionType.fight:
                if (hero == Hero.adventurer && sApts > 0)
                {
                    cost -= sApts;
                    sApts--;
                }
                aPts -= cost;
                if(succes)
                {
                    onFight.Invoke();
                }
                else
                {
                    onActionFailed.Invoke();
                }
                onAction.Invoke();
                break;
            case ActionType.repair:
                if (hero == Hero.engineer && sApts > 0)
                {
                    cost -= sApts;
                    sApts--;
                }
                aPts -= cost;
                if (succes)
                {
                    onRepair.Invoke();
                }
                else
                {
                    onActionFailed.Invoke();
                }
                onAction.Invoke();
                break;
            case ActionType.demolition:
                if (hero == Hero.engineer && sApts > 0)
                {
                    cost -= sApts;
                    sApts--;
                }
                aPts -= cost;
                if (succes)
                {
                    onDemolish.Invoke();
                }
                else
                {
                    onActionFailed.Invoke();
                }
                onAction.Invoke();
                break;
            case ActionType.getFood:
                if (hero == Hero.cheef && sApts > 0)
                {
                    cost -= sApts;
                    sApts--;
                }
                aPts -= cost;
                if (succes)
                {
                    onGatherFood.Invoke();
                }
                else
                {
                    onActionFailed.Invoke();
                }
                onAction.Invoke();
                break;
            case ActionType.invent:
                if (hero == Hero.scientist && sApts > 0)
                {
                    cost -= sApts;
                    sApts--;
                }
                aPts -= cost;
                if (succes)
                {
                    onInvent.Invoke();
                }
                else
                {
                    onActionFailed.Invoke();
                }
                onAction.Invoke();
                break;
            case ActionType.none:
                aPts -= cost;
                if(!succes)
                {
                    onActionFailed.Invoke();
                }
                onAction.Invoke();
                break;
            default:
                aPts -= cost;
                if (!succes)
                {
                    onActionFailed.Invoke();
                }
                onAction.Invoke();
                break;
        }
    }
    public RollDiceResult RollDice()
    {
        int roll = 0;
        switch(morale)
        {
            case -1:
                roll += Random.Range(0, 2) == 1 ? 1 : 0;//succes or fail
                roll += 2;//dmg
                roll += Random.Range(0, 3) == 1 ? 4 : 0;//random event chance
                break;
            case 0:
                roll += Random.Range(0, 2) == 1 ? 1 : 0;//succes or fail
                roll += Random.Range(0, 3) == 1 ? 2 : 0;//dmg chance
                roll += Random.Range(0, 3) == 1 ? 4 : 0;//random event chance
                break;
            case 1:
                roll += 1;//succes
                roll += Random.Range(0, 3) == 1 ? 2 : 0;//dmg chance
                roll += Random.Range(0, 3) == 1 ? 4 : 0;//random event chance
                break;

        }
        return (RollDiceResult)roll;
    }
    //others
    private int MoveActionPts()
    {
        if(hero == Hero.adventurer)
        {
            return aPts + sApts;
        }
        return aPts;
    }
    private int FightActionPts()
    {
        if (hero == Hero.adventurer)
        {
            return aPts + sApts;
        }
        return aPts;
    }
    private int RepairActionPts()
    {
        if(hero == Hero.engineer)
        {
            return aPts + sApts;
        }
        return aPts;
    }
    private int DemolitionActionPts()
    {
        if (hero == Hero.engineer)
        {
            return aPts + sApts;
        }
        return aPts;
    }
    private int GetFoodActionPts()
    {
        if (hero == Hero.cheef)
        {
            return aPts + sApts;
        }
        return aPts;
    }
    private int InventActionPts()
    {
        if (hero == Hero.scientist)
        {
            return aPts + sApts;
        }
        return aPts;
    }
    private void MovePlayer()
    {
        Vector3 targetPos = TileMap.Instance().tileMap[pos.x, pos.y].transform.position + Vector3.up;
        float speed = markerSpeed.Evaluate(Vector2.Distance(new Vector2(targetPos.x, targetPos.z), new Vector2(transform.position.x, transform.position.z)));
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * 0.02f);
        if(Vector3.Distance(targetPos, transform.position)<0.5f)
        {
            CancelInvoke("MovePlayer");
        }
    }

    public int GetHp() {
        return hp;
    }
    
    public int GetMorale() {
        return morale;
    }

    public int GetAPts() {
        return aPts;
    }

    public int GetSAPts() {
        return sApts;
    }
}
public enum Hero
{
    cheef,
    scientist,
    engineer,
    adventurer
}
