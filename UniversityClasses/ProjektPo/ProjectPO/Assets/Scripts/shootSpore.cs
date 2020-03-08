using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootSpore : MonoBehaviour
{
    //varaible
    public GameObject projectile; //object that will be shoot
    public float shootTime; //how ofen cannon can shoot
    public int chanceShoot; //chance of shooting a projectile
    public Transform shootFrom; //location of the object form projectile will be fired
    public GameObject shootFX; //shoot efects spawned on cannon shoot
    public AudioClip shootSound; //sound played on cannon shoot
    private float nextShootTime; //when it can shoot again
    private Animator cannonAnim; //reference to cannon part which will shot
    
    
    void Start() {
        //initialization
        cannonAnim = GetComponentInChildren<Animator>();
        nextShootTime = 0f;
    }

    void OnTriggerStay2D(Collider2D other)  {
        //if there is a player and we can shoot
        if(other.tag == "Player" && nextShootTime < Time.time) {
            //reset next shoot
            nextShootTime = Time.time + shootTime;
            //shooting
            if(Random.Range(0, 10) <= chanceShoot) {
                //spawning projectile
                Instantiate(projectile, shootFrom.position, Quaternion.identity);
                //spawning shoot efects
                Instantiate(shootFX, shootFrom.position, shootFrom.rotation);
                //playing sound
                AudioSource.PlayClipAtPoint(shootSound, shootFrom.position);
                //setting right animation
                cannonAnim.SetTrigger("cannonShoot");
            }
        }
    }
}
