using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    //references
    private Movement movement;          //module responsible for all character movement
    [SerializeField]
    Canvas inventory;                   //players canvas for inventory
    CameraFollow  cF;                   //camera follow script reference 
    CanInteract interact;               //caninteract script reference
    Damagable dmg;                      //damagable script reference
    GameObject focus;                   //gameobject needed for quiting focus on range
    Behaviour halo;                     //halo displayed on focused enemy

    //variables
    private Vector3 direction;          //direction vector made from player's input
    List<GameObject> enemies;
    int enemyIndex;
    
    void Start() {
        //getting Movement script reference
        movement = GetComponent<Movement>();
        cF = Camera.main.gameObject.GetComponent<CameraFollow>();
        interact = GetComponent<CanInteract>();
        enemies = new List<GameObject>();
        enemyIndex = -1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        //making direction vector from input
        direction = new Vector3(Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal")).normalized;
        if (direction.x < 0.0f)
        {
            direction.Scale(new Vector3(0.8f, 1, 1));
        }
        direction = transform.right * direction.z + transform.forward * direction.x;
        //moving player
        
        movement.moveVec = direction;
        //checking if player jumped
        if(Input.GetButtonDown("Jump")) {
            movement.jump = true;
        }
        //checking if player dashed
        if (Input.GetButtonDown("Sprint"))
        {
            movement.dash = true;
            movement.jump = true;
        }
        //checking if player makes longer jump
        movement.longJump = Input.GetButton("Jump");

        if(Input.GetKeyDown(KeyCode.I)) {
            InventorySetup();
        }
        
        if (Input.GetKeyDown(KeyCode.E)) {
            interact.Interact();
        }

        if(Input.GetKeyDown(KeyCode.Q)) { 
            Focus();
        }
        if(Input.GetMouseButtonDown(0) && !inventory.gameObject.activeSelf)
        {
            movement.lightAttack = true;
        }
        /*
        //cheking if player rolled
        if (Input.GetButtonDown("Roll")) {
            movement.roll = true;
        }
        */
        enemies.RemoveAll(a => a == null);

        if(focus == null || dmg.died)
        {
            cF.DeFocus();
            movement.DeFocus();
        }
    }

    void InventorySetup() {
        GameObject inv = inventory.gameObject;
        inv.SetActive(!inv.activeSelf);
        Cursor.visible = inv.activeSelf;
        if(inv.activeSelf)  { 
            Cursor.lockState = CursorLockMode.None;
        } else {
            Cursor.lockState = CursorLockMode.Locked;
        }
        movement.canMove = !movement.canMove;
        cF.canRotate = !cF.canRotate;
        cF.InventoryCamera();
    }

    void Focus() {
        if(halo) {
            halo.enabled = false;
        }

        if(enemies.Count > 0) {
            enemyIndex++;
            if(enemyIndex % (enemies.Count + 1) == enemies.Count) {
                cF.DeFocus();
                focus = null;
                movement.DeFocus();
            } else {
                cF.Focus(enemies[enemyIndex % enemies.Count].transform);
                movement.Focus(enemies[enemyIndex % enemies.Count].transform);
                focus = enemies[enemyIndex % enemies.Count];
                dmg = focus.GetComponent<Damagable>();
                halo = GetHalo(focus);
                halo.enabled = true;
            }
        }
        else {
            cF.DeFocus();
            movement.DeFocus();
            focus = null;
        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.tag == "Enemy" && !enemies.Contains(other.gameObject)){
            enemies.Add(other.gameObject);
        }    
    }

    void OnTriggerExit(Collider other) {
        if(other.tag == "Enemy" && enemies.Contains(other.gameObject)) {
            enemies.Remove(other.gameObject);
        }
    }

    Behaviour GetHalo(GameObject obj) {
        Transform son = obj.transform.Find("HaloHolder");
        Behaviour enemHalo = (Behaviour)son.GetComponent("Halo");
        return enemHalo;
    }
}
