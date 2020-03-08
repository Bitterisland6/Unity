using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneManager : MonoBehaviour
{
    [SerializeField]
    Animator anim;
    float animdur = 48f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > animdur) {
            anim.enabled = false;
        }
    }
}
