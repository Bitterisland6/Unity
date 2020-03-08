using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(HumanoidAnimatorParamBase))]
public class HumanoidAnimator : MonoBehaviour
{
    HumanoidAnimatorParamBase parameters;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        parameters = GetComponent<HumanoidAnimatorParamBase>();
        
        if (gameObject.tag == "Player")
        {
            animator = GetComponentInChildren<Animator>();
        }
        else
        {
            animator = GetComponentsInChildren<Animator>()[1];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(animator != null)
        {
            UpdateMoveSpeed();
            UpdateIsGrounded();
            UpdateIsRolling();
            UpdateLightAttack();
            UpdateLandAttack();
        }
    }
    void UpdateMoveSpeed()
    {
        Vector3 moveSpeed = parameters.MoveSpeed();
        animator.SetFloat("xSpeed", moveSpeed.x);
        animator.SetFloat("ySpeed", moveSpeed.y);
        animator.SetFloat("zSpeed", moveSpeed.z);
    }
    void UpdateIsGrounded()
    {
        animator.SetBool("isGrounded", parameters.IsGrounded());
    }
    void UpdateIsRolling()
    {
        animator.SetBool("isRolling", parameters.IsRolling());
    }
    void UpdateLightAttack()
    {
        animator.SetInteger("comboLight", parameters.LightAttack());
    }
    void UpdateLandAttack()
    {
        animator.SetBool("landAttack", parameters.LandAttack());
    }
}
