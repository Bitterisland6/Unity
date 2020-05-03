using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUIUpdate : GameEventListener 
{    
    [SerializeField]
    Image[] morale;
    [SerializeField]
    Image[] fullHearts;
    [SerializeField]
    Image[] emptyHearts;
    [SerializeField]
    Image[] fullApts;
    [SerializeField]
    Image[] emptyApts;
    [SerializeField]
    Image[] fullSApts;
    [SerializeField]
    Image[] emptySApts;
    [SerializeField]
    Image  heroImage;
    [SerializeField]
    Canvas playerUI;
    [SerializeField]
    Image sliderHealth;
 
    //privaet varaibles
    Player activePlayer;
    int maxHealth;
    int health;
    int plMorale;
    int maxAPts;
    int aPts;
    int maxSApts;
    int sAPts;


    void Start() {
        action.AddListener(UpdateUI);
        UpdateUI();
    }

    public void UpdateUI() {
        if(PlayerGroup.Instance().CurrentPlayer() == null) {
            HideUI();
            return;
        } else {
            if(activePlayer != null) {
                activePlayer.onDamaged.RemoveListener(UpdateHearts);
                activePlayer.onHealed.RemoveListener(UpdateHearts);
                activePlayer.onMoraleUp.RemoveListener(UpdateMorale);
                activePlayer.onMoraleDown.RemoveListener(UpdateMorale);
                activePlayer.onAction.RemoveListener(UpdatePts);
            }
            ShowUI();
            activePlayer = PlayerGroup.Instance().CurrentPlayer();
            activePlayer.onDamaged.AddListener(UpdateHearts);
            activePlayer.onHealed.AddListener(UpdateHearts);
            activePlayer.onMoraleUp.AddListener(UpdateMorale);
            activePlayer.onMoraleDown.AddListener(UpdateMorale);
            activePlayer.onAction.AddListener(UpdatePts);
            UpdateHearts();
            UpdateMorale();
            UpdatePts();
            heroImage.sprite = activePlayer.sprite;
        }
    }

    private void HideUI() {
        playerUI.gameObject.SetActive(false);
    }

    private void ShowUI() {
        playerUI.gameObject.SetActive(true);
    }

    private void UpdateHearts() {
        if(activePlayer != null) {
            int maxHealth = activePlayer.hpMax;
            int health = activePlayer.GetHp();
            
            for(int hp = 0; hp < fullHearts.Length; hp++) {
                if(hp < maxHealth) {
                    if(hp < health) {
                        fullHearts[hp].gameObject.SetActive(true);
                        emptyHearts[hp].gameObject.SetActive(false);
                    } else {
                        fullHearts[hp].gameObject.SetActive(false);
                        emptyHearts[hp].gameObject.SetActive(true);
                    }
                } else {
                    fullHearts[hp].gameObject.SetActive(false);
                    emptyHearts[hp].gameObject.SetActive(false);
                }    
            }
        }
    }

    private void UpdateMorale() {
        if(activePlayer != null) {
            plMorale = activePlayer.GetMorale();
            for(int m = 0; m < morale.Length; m++) {
                morale[m].gameObject.SetActive(false);
            }
            morale[plMorale + 1].gameObject.SetActive(true);
        }
    }

    private void UpdatePts() {
        if(activePlayer != null) {
            maxAPts = activePlayer.aPtsMax;
            aPts = activePlayer.GetAPts();
            maxSApts = activePlayer.specialAPtsMax;
            sAPts = activePlayer.GetSAPts();
            for (int ap = 0; ap < fullApts.Length; ap++) {
                if(ap < maxAPts) {
                    if(ap < aPts) {
                        fullApts[ap].gameObject.SetActive(true);
                        emptyApts[ap].gameObject.SetActive(false);
                    } else {
                        fullApts[ap].gameObject.SetActive(false);
                        emptyApts[ap].gameObject.SetActive(true);
                    }
                } else {
                    fullApts[ap].gameObject.SetActive(false);
                    emptyApts[ap].gameObject.SetActive(false);
                }
            }

            for(int sp = 0; sp < fullSApts.Length; sp++) {
                if(sp < maxSApts) {
                    if(sp < sAPts) {
                        fullSApts[sp].gameObject.SetActive(true);
                        emptySApts[sp].gameObject.SetActive(false);
                    } else {
                        fullSApts[sp].gameObject.SetActive(false);
                        emptySApts[sp].gameObject.SetActive(true);
                    }
                } else {
                    fullSApts[sp].gameObject.SetActive(false);
                    emptySApts[sp].gameObject.SetActive(false);
                }
            }
        }
    }
}
