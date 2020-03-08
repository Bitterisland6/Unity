using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    public Animator animator;
    public CharacterController chC;

    public bool endSceneWalk = false;
    public bool endSceneReach = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 animatorVec = Quaternion.Inverse(transform.rotation) * chC.velocity;
        animator.SetFloat("xSpeed", animatorVec.x);
        animator.SetFloat("zSpeed", animatorVec.z);
        if(endSceneWalk) {
            EndSceneWalk();
        }
        if(endSceneReach) {
            EndSceneReach();
        }
    }

    public void EndSceneWalk() {
        animator.SetFloat("xSpeed", 0f);
        animator.SetFloat("zSpeed", 1f);
    }

    public void EndSceneReach() {
        animator.Play("Reach");
    }
}
