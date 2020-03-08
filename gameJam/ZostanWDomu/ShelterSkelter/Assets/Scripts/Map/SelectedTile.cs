using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedTile : GameEventListener
{
    public Image[] moveAPTS;
    public Image[] moveNoRiskAPTS;
    public Image[] actionAPTS;
    public Image[] actionNoRiskAPTS;
    public Image moveIcon;
    public Image moveNoRiskIcon;
    public Image actionIcon;
    public Image actionNoRiskIcon;
    [HideInInspector]
    public Tile selected;
    public GameObject tileActionsCanvas;
    private FollowTarget canvFollow;
    public WSButton move;
    public WSButton moveNoRisk;
    public WSButton act;
    public WSButton actNoRisk;
    [SerializeField]
    Shader selectShader;
    static SelectedTile instance;
    Player player;
    void Start()
    {
        canvFollow = tileActionsCanvas.GetComponent<FollowTarget>();
        instance = this;
        action.AddListener(PhaseUpdate);

        move.onClick.AddListener(MoveToTile);
        moveNoRisk.onClick.AddListener(MoveToTileNoRisk);
        act.onClick.AddListener(Action);
        actNoRisk.onClick.AddListener(ActionNoRisk);
    }
    private void PhaseUpdate()
    {
        player = PlayerGroup.Instance().CurrentPlayer();
        if(player == null)
        {
            tileActionsCanvas.SetActive(false);
        }
        else
        {
            UIUpdate();
        }
    }
    public void CancelSelection() {
        if(selected)
            selected.ResetShader();
        tileActionsCanvas.SetActive(false);
    }
    public void Selection() {
        if(selected) {
            selected.ChangeShader(selectShader);
        }
        UIUpdate();
    }

    public static SelectedTile Instance() {
        if(instance == null) {
            Debug.LogError("SelectedTile doesn't exists");
        }
        return instance;
    }

    private void UIUpdate()
    {
        if(player != null && selected != null)
        {
            canvFollow.target = selected.transform.position;

            Sprite m = selected.moveAction?.icon;
            Sprite a = selected.tileAction?.icon;
            //reset
            for(int i=0; i<3; i++)
            {
                moveAPTS[i].enabled = false;
                moveNoRiskAPTS[i].enabled = false;
                actionAPTS[i].enabled = false;
                actionNoRiskAPTS[i].enabled = false;
            }
            //set
            if(m != null)
            {
                moveIcon.sprite = m;
                for(int i = 0; i < selected.moveAction.aPtsCost; i++)
                {
                    moveAPTS[i].enabled = true;
                }
                moveNoRiskIcon.sprite = m;
                for (int i = 0; i < selected.moveAction.aPtsCost+1; i++)
                {
                    moveNoRiskAPTS[i].enabled = true;
                }
            }
            if (a != null)
            {
                actionIcon.sprite = a;
                for (int i = 0; i < selected.tileAction.aPtsCost; i++)
                {
                    actionAPTS[i].enabled = true;
                }
                actionNoRiskIcon.sprite = a;
                for (int i = 0; i < selected.tileAction.aPtsCost + 1; i++)
                {
                    actionNoRiskAPTS[i].enabled = true;
                }
            }
            tileActionsCanvas.SetActive(true);
            move.gameObject.SetActive(CanMove(false));
            moveNoRisk.gameObject.SetActive(CanMove(true));
            act.gameObject.SetActive(CanAct(false));
            actNoRisk.gameObject.SetActive(CanAct(true));
            Invoke("LateUIUpdate", 0.02f);
        }
        else
        {
            move.gameObject.SetActive(false);
            moveNoRisk.gameObject.SetActive(false);
            act.gameObject.SetActive(false);
            actNoRisk.gameObject.SetActive(false);
        }
    }

    //UI functions
    private void MoveToTile()
    {
        if (selected == null) return;
        selected.MoveHere(player);
        UIUpdate();
    }
    private void MoveToTileNoRisk()
    {
        if (selected == null) return;
        selected.MoveHere(player, true);
        UIUpdate();
    }
    private void Action()
    {
        selected.PerformAction(player);
        UIUpdate();
    }
    private void ActionNoRisk()
    {
        selected.PerformAction(player, true);
        UIUpdate();
    }
    //UI helper
    private bool CanMove(bool noRisk)
    {
        return selected.CanMoveHere(player, noRisk);
    }
    private bool CanAct(bool noRisk)
    {
        return selected.CanPerformAction(player, noRisk);
    }
    private void LateUIUpdate()
    {
        if(selected == null)
        {
            UIUpdate();
        }
    }
}
