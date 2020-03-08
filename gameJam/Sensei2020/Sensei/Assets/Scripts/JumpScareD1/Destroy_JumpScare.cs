using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_JumpScare : MonoBehaviour
{
    public GameObject mann;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered");
        if (other.CompareTag("JumpScare_Mannekin"))
        {
            CharacterAnimator anim = other.gameObject.GetComponent<CharacterAnimator>();
            anim.endSceneWalk = false;
            mann.SetActive(false);
        }
    }
}
