using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverHealth : MonoBehaviour
{
    [SerializeField]
    int CoverEndurance; //how many shots can cover take

    void Update() {
        if(CoverEndurance == 0) {
            Destroy(gameObject);
        }
    }

    //function damagind cover
    public void DmgCover() {
        CoverEndurance--;
    }
}
