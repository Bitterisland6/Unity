using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    //variables
    public float fullHealth; //maximum amount of health player will have
    private float currentHealth; //current amount of player's health;
    public GameObject deathFX; //efects instantiate on player's death

    //HUD variables
    public Slider helathSlider; //refernece to GUI slider
    public Image damageScreen; //image flashin on screen when player get damage
    public Text gameOverScreen; //text displayed when player dies
    public Text winGameScreen; //text displayed when player wins the game
    private bool damaged = false; //checking if player get damage, will be used to flash damage screen
    private Color damagedColor = new Color(1f, 1f, 1f, 0.5f); //how much the scrren will flash
    private float smoothColor = 5f; //cooldown of image flash

    //sound variables
    public AudioClip playerHurt; //sound played when player gets damage
    private AudioSource playerAS; //reference to players audiosource
    public AudioClip playerDeadSong; //song played on player's death
    public GameObject gameManager; //reference to object playing main song in game

    //restarting the game
    public restartGame restartManager; //reference needed to call restart function in restartGame script
    
    void Start() {
        //initialization
        currentHealth = fullHealth;

        //HUD initialization
        helathSlider.maxValue = fullHealth;
        helathSlider.value = fullHealth;

        //audio initialization
        playerAS = GetComponent<AudioSource>();
    }

    void Update() {
        //if player gets damage, flash screen
        if(damaged) {
            damageScreen.color = damagedColor;
            damaged = false;
        }
        else {//turn smoothly damage screen off
            damageScreen.color = Color.Lerp(damageScreen.color, Color.clear, smoothColor*Time.deltaTime);
        }
    }

    //function that will allow other objects to deal damage to player
    public void addDamage(float damage) {
        //object deals 0 damage
        if(damage <= 0)
            return;
        else //object deals damage
            currentHealth -= damage;
        //setting audio source clip to chosen clip
        playerAS.PlayOneShot(playerHurt);

        helathSlider.value = currentHealth;
        damaged = true;
        //if the player is dead
        if(currentHealth <= 0)
            makeDead(gameManager);
    }

    //function healing given health amount 
    public void addHealth(float healthAmount) {
        //adding health
        currentHealth += healthAmount;
        //if current health is bigger than max health
        if(currentHealth >= fullHealth)
            currentHealth = fullHealth;
        //setting slider value
        helathSlider.value = currentHealth;
    }

    //function that makes player dead
    //it's public because sometimes player will die instantly (e.g. when he falls of the edge)
    public void makeDead(GameObject GO) {
        //changing song
        mainSongController controller = GO.GetComponent<mainSongController>();
        controller.PlaySong(playerDeadSong, 1f);
        //showing death effects
        Instantiate(deathFX, transform.position, transform.rotation);
        //destroying player
        Destroy(gameObject);
        damageScreen.color = damagedColor;

        //access to animator of text displayed when player dies
        Animator gameOverAnimator = gameOverScreen.GetComponent<Animator>();
        gameOverAnimator.SetTrigger("gameOver");

        //restarting the game
        restartManager.restart();
    }

    public void winGame() {
        //destroying player
        Destroy(gameObject);
        //setting possibility to restart
        restartManager.restart();

        Animator winGameAnimator = winGameScreen.GetComponent<Animator>();
        winGameAnimator.SetTrigger("gameOver");    
    }
}
