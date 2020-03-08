using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpCharacter : MonoBehaviour
{
    //variables
    [SerializeField]
    GameObject poofFX;                           //poof efect spawned on character disapearing
    [SerializeField]
    CharacterAnimator characterAnim;             //character animator
    [SerializeField]
    AudioClip sound;                             //heart beat sound

    private bool destroying = false;             //flag saying if the character is on the way to disapear
    private float destroyTime = 5f;              //destroy offset


    void Update() {
        if(destroying){ 
            if(Time.time > destroyTime) {
                characterAnim.endSceneReach = false;
                Destroy(characterAnim.gameObject);
                Instantiate(poofFX, transform.position, Quaternion.identity);
                Destroy(gameObject, 3f);
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.tag == "Player" && !destroying) {
            InvokeRepeating("PlaySound", 0f, 2f);
            characterAnim.gameObject.transform.LookAt(other.gameObject.transform);
            characterAnim.endSceneReach = true;
            destroying = true;
            destroyTime += Time.time;
            
        }
        
    }

    private void PlaySound() {
        AudioSource.PlayClipAtPoint(sound, transform.position);
    }
}
