using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : HumanoidAnimatorParamBase
{
    [Header("Visuals")]
    public ParticleSystem jumpPuff;
    public ParticleSystem dashPuff;
    public ParticleSystem explosion;
    public AudioClip puff;


    //flags & attributes
    [HideInInspector]
    public bool canMove;                //humanoid can move
    [HideInInspector]
    public bool canJump;                //humanoid can jump
    [HideInInspector]
    public bool canRoll;                //humanoid can roll
    public float speed = 1f;            //speed multiplier based by attributes
    public float rotSpeed = 1f;

    //set in controller
    [HideInInspector]
    public Vector3 moveVec;             //vector of move (direction and speed) of humanoid
    [HideInInspector]
    public bool jump;                   //flag saying when humanoid jumped
    [HideInInspector]
    public bool longJump;               //flag saying when humanoid made a longer jump
    [HideInInspector]
    public bool roll;                   //flag saying when humanoid rolled
    [HideInInspector]
    public bool dash;
    [HideInInspector]
    public bool lightAttack;

    //references
    private Rigidbody rb;               //humanoid's rigidbody
    public Transform originalLookAt;    //humanoid attractor - object which humanoid is looking at
    public Transform lookAt;

    [Header("Jump")]
    //jumping variables
    [SerializeField]
    LayerMask groundLayer;              //ground's layer - saying what ground is for the humanoid
    private float groundCheckRadius;    //radius of ground checking overlap sphere
    [SerializeField]
    Transform groundCheck;              //placement of component detecting collision with ground
    Collider[] detectedColliders;       //colliders detected by groundcheck
    private bool canDoubleJump;         //flag saying when humanoid can make double jump
    [SerializeField]
    private float inAirSpeedMult;       //if player is in air this multiplayer will effect its speed (below 1 is recommended)
    public float jumpForce = 1f;        //jump force multiplier based by attributes
    public float fallMultiplier = 50f;
    [Range(0, 1)]
    public float lowJumpMultiplier = 2f;
    [HideInInspector]
    public bool isGrounded = true;      //humanoid stands on the ground

    //dash variables
    [Header("Dash")]
    [SerializeField]
    float dashForce;
    [SerializeField]
    float dashCD;
    float lastDash;

    //combat
    [Header("Combat")]
    private float lastComb;
    public float comboPerfection; //if combo time is 2.0 and comboPerfection = 0.2 then anything between 1.8 and 2.2 will trigger combo
    [Header("Light Attack")]
    //light attack
    public int comboLight;
    public float combT1Light;
    public float combT2Light;
    public float endT1Light;
    public float endT2Light;
    public float damageT1Light;
    public float damageT2Light;
    private bool damageDealt;
    public float rangeLight;
    public float damageLight;
    public float knockbackLight;
    [Range(0,1.0f)]
    public float slowWalk;

    [Header("Land Attack")]
    public float endTLandAttack;
    private bool landAttacking;
    private bool landed;
    public float explosionRadius;
    public float explosionForce;
    public float explosionDamage;
    

    public bool canAttack;

    bool controllEnabled = true;
    void Awake() {
        //flags initialization
        canMove = true;
        canJump = true;
        canRoll = true;
        isGrounded = true;
        canDoubleJump = false;
        groundCheckRadius = 0.2f;
        //getting rigidbody reference
        rb = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate() {
        if(groundCheck != null)
        {
            detectedColliders = Physics.OverlapSphere(groundCheck.position, groundCheckRadius);
            bool check = false;
            foreach (Collider col in detectedColliders)
            {
                if (!col.isTrigger && col.gameObject.tag != gameObject.tag)
                {
                    check = true;
                    break;
                }
            }

            if (check)
            {
                isGrounded = true;
                canDoubleJump = true;
            }
            else
            {
                isGrounded = false;
            }
        }
        
        if (!isGrounded && rb.velocity.y < -0.1)
        {
            //faster falling
            rb.AddForce(Physics.gravity * rb.mass); // Krzyśkowe oparte o siły (jak reszta komponentu) odpowiada podwojeniu grawitacji na czas spadania
            if(landAttacking)
            {
                rb.AddForce(Physics.gravity * rb.mass);
            }
        }
        else if (rb.velocity.y > 0.01 && longJump)
        {
            rb.AddForce(-Physics.gravity * rb.mass * lowJumpMultiplier);
        }
        if(isGrounded)
        {
            rb.AddForce(-Physics.gravity * rb.mass * 0.5f);
        }

        //active movement actions
        //moving humanoid
        if (canMove && !landAttacking)
        {
            Move(moveVec);
        }
        //dash
        if (dash)
        {
            if ((isGrounded || canDoubleJump) && Time.time - lastDash > dashCD && comboLight == 0 && !landAttacking)
            {
                Dash();
            }
        }
        //making humanoid jump
        if (jump)
        {
            if (canJump && comboLight == 0 && !landAttacking)
            {
                Jump();
            }
        }
        //rolling humanoid
        if (roll)
        {
            if (isGrounded && canRoll && comboLight == 0 && !landAttacking)
            {
                Roll();
            }
        }
        //turning humanoid right direction
        if (lookAt != null && !landAttacking)
        {
            //transform.LookAt(lookAt);
            Vector3 pos = lookAt.position - transform.position;
            var newRot = Quaternion.LookRotation(pos);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRot, rotSpeed);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
        //active combat actions
        if (lightAttack)
        {
            if(isGrounded)
            {
                AttackLight();
            }
            else
            {
                AttackLand();
            }
        }
        //combat state dependend 
        AttackLightUpdate();
        LandAttackUpdate();
        //reseting input
        ResetInput();
    }
    private void ResetInput()
    {
        jump = false;
        roll = false;                   
        dash = false;
        lightAttack = false;
    }
    //function making humanoid jump
    private void Jump() {
        //making double jump possible
       if(isGrounded){
            isGrounded = false;
            canDoubleJump = true;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
       } else if(canDoubleJump) {
            canDoubleJump = false;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce * 0.8f, ForceMode.Impulse);
            jumpPuff.Play();
            AudioSource.PlayClipAtPoint(puff, transform.position);
        }
    }

    private void Roll() {
        throw new NotImplementedException();
    }

    //function making humanoid move by given vector
    private void Move(Vector3 moveVec) {
        rb.AddForce(moveVec * speed * (isGrounded ? 1 : inAirSpeedMult) * ((comboLight == 0) ? 1 : slowWalk));
    }
    
    private void Dash() {
        lastDash = Time.time;
        rb.AddForce(transform.forward * dashForce, ForceMode.Impulse);
        dashPuff.Play();
        AudioSource.PlayClipAtPoint(puff, transform.position);
    }

    private void AttackLight()
    {
        switch(comboLight)
        {
            case 0:
                comboLight = 1;
                damageDealt = false;
                CombTUp();
                break;
            case 1:
                if(ComboCheck(combT1Light))
                {
                    comboLight = 2;
                    damageDealt = false;
                    CombTUp();
                }
                else
                {
                    comboLight = 3;
                }
                break;
            case 2:
                if (ComboCheck(combT2Light))
                {
                    comboLight = 1;
                    damageDealt = false;
                    CombTUp();
                }
                else
                {
                    comboLight = 4;
                }
                break;
            default:
                break;
        }
    }
    private void AttackLightUpdate()
    {
        switch(comboLight)
        {
            case 0:
                break;
            case 1:
                if(EndCheck(endT1Light))
                {
                    comboLight = 0;
                }
                if (!damageDealt && Time.time - lastComb > damageT1Light)
                {
                    DealLightDamage();
                }
                break;
            case 2:
                if (EndCheck(endT2Light))
                {
                    comboLight = 0;
                }
                if (!damageDealt && Time.time - lastComb > damageT2Light)
                {
                    DealLightDamage();
                }
                break;
            case 3:
                if (EndCheck(endT1Light))
                {
                    comboLight = 0;
                }
                if (!damageDealt && Time.time - lastComb > damageT1Light)
                {
                    DealLightDamage();
                }
                break;
            case 4:
                if (EndCheck(endT2Light))
                {
                    comboLight = 0;
                }
                if (!damageDealt && Time.time - lastComb > damageT2Light)
                {
                    DealLightDamage();
                }
                break;
        }
    }
    private void DealLightDamage()
    {
        damageDealt = true;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position + (rangeLight / 2) * transform.forward, rangeLight / 2);
        List<GameObject> obj = new List<GameObject>();
        foreach (Collider col in hitColliders)
        {
            if (!col.isTrigger && gameObject.tag != col.gameObject.tag && !obj.Contains(col.gameObject))
            {
                obj.Add(col.gameObject);
            }
        }
        foreach (GameObject o in obj)
        {
            if (o.TryGetComponent<Rigidbody>(out Rigidbody r))
            {
                r.AddExplosionForce(knockbackLight, transform.position, rangeLight * 2);
            }

            if (o.TryGetComponent<Damagable>(out Damagable d))
            {
                d.Damage(damageLight);
            }
        }
    }
    private void AttackLand()
    {
        landAttacking = true;
        landed = false;
    }
    private void LandAttackUpdate()
    {
        if(landAttacking && isGrounded && !landed)
        {
            CombTUp();
            landed = true;
            explosion.Play();
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
            List<GameObject> obj = new List<GameObject>();
            foreach (Collider col in hitColliders)
            {
                if (!col.isTrigger && gameObject.tag != col.gameObject.tag && !obj.Contains(col.gameObject))
                {
                    obj.Add(col.gameObject);
                }
            }
            foreach (GameObject o in obj)
            {
                if(o.TryGetComponent<Rigidbody>(out Rigidbody r))
                {
                    r.AddExplosionForce(explosionForce, transform.position - Vector3.up, explosionRadius);
                }
                if (o.TryGetComponent<Damagable>(out Damagable d))
                {
                    d.Damage(explosionDamage);
                }
            }
            AudioSource.PlayClipAtPoint(puff, transform.position);
        }
        if (landed && EndCheck(endTLandAttack))
        {
            landAttacking = false;
            landed = false;
        }
    }
    private bool ComboCheck(float combT)
    {
        float delta = Time.time - lastComb;
        return delta < combT + comboPerfection && delta > combT - comboPerfection;
    }
    private bool EndCheck(float endT)
    {
        float delta = Time.time - lastComb;
        return delta > endT;
    }
    private void CombTUp()
    {
        lastComb = Time.time;
    }

    public void Focus(Transform target) {
        lookAt = target;
    }

    public void DeFocus() {
        lookAt = originalLookAt;
    }

    public override bool IsGrounded() {
        return isGrounded;
    }

    public override bool IsRolling() {
        return false;
    }

    //function giving humanoid velocity to animator
    public override Vector3 MoveSpeed() {
        //hardcoded vector for blendtree basd on player's velocity
        Vector3 animDir = new Vector3(rb.velocity.z, rb.velocity.y, -rb.velocity.x);
        //returned hardocoded vector rotated by rotation opposite to player's rotation
        return Quaternion.Inverse(transform.rotation) * animDir;
    }
    override public int LightAttack()
    {
        return comboLight;
    }
    public override bool LandAttack()
    {
        return landAttacking;
    }
}
