using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourcesUIUpdate : GameEventListener
{
    [SerializeField]
    TextMeshProUGUI metalTxt;
    [SerializeField]
    TextMeshProUGUI bricksTxt;
    [SerializeField]
    TextMeshProUGUI woodTxt;
    [SerializeField]
    TextMeshProUGUI foodTxt;

    Bank activeBankinstance;

    //private variables
    private int food   = -1;
    private int bricks = -1;
    private int wood   = -1;
    private int metal  = -1;

    void Start() {
        action.AddListener(UpdateResources);
        UpdateResources();
    }

    public void UpdateResources() {
        if(Bank.Instance() == null) {
            return;
        } else {
            if(activeBankinstance != null) {
                activeBankinstance.onResourceChanged.RemoveListener(UpdateTxt);
            }
            activeBankinstance = Bank.Instance();
            activeBankinstance.onResourceChanged.AddListener(UpdateTxt);
            UpdateTxt();
        }
    }

    private void UpdateFood() {
        if(food == -1 || food != Bank.Instance().food) {
            food = Bank.Instance().food;
            foodTxt.text = food.ToString();
        }
    }
    

    private void UpdateBricks() {
        if(bricks == -1 || bricks != Bank.Instance().bricks) {
            bricks = Bank.Instance().bricks;
            bricksTxt.text = bricks.ToString();
        }
    }

    private void UpdateMetal() {
        if(metal == -1 || metal != Bank.Instance().metal){
            metal = Bank.Instance().metal;
            metalTxt.text = metal.ToString();
        }
    }

    private void UpdateWood() {
        if(wood == -1 || wood != Bank.Instance().wood) {
            wood = Bank.Instance().wood;
            woodTxt.text = wood.ToString();
        }
    }

    private void UpdateTxt(){
            UpdateBricks();
            UpdateMetal();
            UpdateWood();
            UpdateFood();
    }
}
