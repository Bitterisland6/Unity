using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Damagable : MonoBehaviour
{
    public float maxHp;
    public float hpRegen;
    public float hp;
    public bool bossActive = false;

    //slider values
    [SerializeField]
    Slider healthSlider;
    [SerializeField]
    Color maxHpColor;
    [SerializeField]
    Color lowHpColor;
    [SerializeField]
    bool playerSlider;
    [SerializeField]
    bool bossSlider;
    [SerializeField]
    Image sliderFill;
    [SerializeField]
    Image playerDamageImage;
    [SerializeField]
    Color damageColor;
    [SerializeField]
    ParticleSystem deathFX;
    [SerializeField]
    ParticleSystem hitFX;
    [SerializeField]
    AudioClip deathSound;
    [SerializeField]
    AudioClip hitSound;
    [HideInInspector]
    public bool died;
    float smoothColor;
    bool damaged;
    RestartGame restart;
    
    
    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
        died = false;

        //slider init and flash screen indicator
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHp;
            healthSlider.value = hp;
        }
        if(!bossSlider) {
            if (sliderFill != null)
                sliderFill.color = maxHpColor;
        }
        if(playerSlider) {
            if (healthSlider != null)
                healthSlider.gameObject.SetActive(true);
        } else{
            if (healthSlider != null)
                healthSlider.gameObject.SetActive(false);
        }    
        damageColor = new Color(255f, 255f, 255f, 0.5f);
        smoothColor = 5f;
        damaged = false;
        

        restart = gameObject.GetComponent<RestartGame>();
    }

    // Update is called once per frame
    void Update()
    {
        hp += hpRegen * Time.deltaTime;
        if(hp > maxHp)
        {
            hp = maxHp;
        }
        if (hp <= 0 && !died)
        {
            Die();
        }
        
        //update of slider's value
        if(healthSlider != null)
        {
            healthSlider.value = hp;
        }
        //setting color of enemy slider or setting active state of boss' slider
        if(!bossSlider) 
        {
            if (sliderFill != null)
                sliderFill.color = Color.Lerp(lowHpColor, maxHpColor, hp/maxHp);
        }
        else 
        {
            if (healthSlider != null)
                healthSlider.gameObject.SetActive(bossActive);
        }
        
        //hiding enemy slider on full hp
        if(!playerSlider && hp == maxHp && !bossSlider) {
            if (healthSlider != null)
                healthSlider.gameObject.SetActive(false);
        }

        //showing hiding damage indicator   
        if(playerSlider) {
            if(damaged && !died) {
                playerDamageImage.color = Color.Lerp(playerDamageImage.color, Color.clear, smoothColor * Time.deltaTime);
            }
            else if(playerDamageImage.color.a == 0f) {
                damaged = false;     
            }
        }
    }
    private void Die()
    {
        if(deathSound != null)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
        }
        if (playerSlider) {
            restart.Restart();
            Time.timeScale = 0;
        }
        else {
            Destroy(gameObject.GetComponent<Collider>());
            Destroy(gameObject.GetComponent<NavMeshAgent>());
        }

        if(deathFX)
        {
            deathFX.Play();
        }
        foreach(Transform child in transform) {
            Destroy(child.gameObject);
        }
        died = true;
        Destroy(gameObject, 10f);
    }

    public void Damage(float dmg)
    {
        hp -= dmg;

        //sound and particle 
        if(hitSound != null)
        {
            AudioSource.PlayClipAtPoint(hitSound, transform.position);
        }
        if (hitFX)
        {
            hitFX.Play();
        }

        //showing slider
        if(!playerSlider && hp != maxHp && !bossSlider) {
            healthSlider.gameObject.SetActive(true);
        }

        //splash damage indicator
        if(playerDamageImage && playerSlider && !died) {
            damageColor = new Color(255f, 255f, 255f, (maxHp - hp) / maxHp);
            playerDamageImage.color = damageColor;
        }

        damaged = true;
    }
}
