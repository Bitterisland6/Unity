using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyHealth : MonoBehaviour
{
    //variables
    public float enemyMaxHealth; //maximum amount of health enemy will have
    private float currentHealth; //current health of enemy
    public GameObject enemyDeathFX; //efects instantiated on enemy's death
    public bool drops; //if enemy can drop heal packs
    public GameObject drop; //object that enemy drops on death

    //HUD variables
    public Slider enemyHealthSlider; //reference to GUI slider

    //aduio variables
    public AudioClip enemyDying; //sound played on enemy's death


    void Start() {
        //initialization
        currentHealth = enemyMaxHealth;

        //HUD initialization
        enemyHealthSlider.maxValue = enemyMaxHealth;
        enemyHealthSlider.value = enemyMaxHealth;
    }

    //function that will allow other objects to change enemy's health
    public void addDamage(float damage) {
        //showing health bar
        enemyHealthSlider.gameObject.SetActive(true);
        //adding damage 
        currentHealth -= damage;
        //seting health bar fill
        enemyHealthSlider.value = currentHealth;

        //if the enemy is dead
        if(currentHealth <= 0)
            makeDead();  
    }

    //function that makes enemy dead
    void makeDead() {
        //playing sound
        AudioSource.PlayClipAtPoint(enemyDying, transform.position);
        //destroying enemy
        Destroy(gameObject.transform.parent.gameObject);
        //spawning death effects
        Instantiate(enemyDeathFX, transform.position, transform.rotation);
        //droping object
        if(drops)
            if(Random.Range(0,10) >= 5)
                Instantiate(drop, transform.position, transform.rotation);
    }
}