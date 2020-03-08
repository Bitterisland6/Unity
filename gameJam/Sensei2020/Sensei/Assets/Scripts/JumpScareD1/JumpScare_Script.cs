using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare_Script : MonoBehaviour
{
    public float speed;
    public GameObject[] manequins;

    void Awake()
    {
        foreach(GameObject obj in manequins)
        {
            CharacterAnimator anim = obj.GetComponent<CharacterAnimator>();
            anim.endSceneWalk = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log("forward");
        foreach(GameObject obj in manequins)
        {    
        obj.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}
