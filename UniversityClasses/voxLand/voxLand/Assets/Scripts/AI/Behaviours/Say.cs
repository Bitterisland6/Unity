using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Say : StateMachineBehaviour
{
    AIInterface ai;
    Damagable dmg;
    
    private GameObject textObject;
    private TextMeshProUGUI text;
    private Canvas canv;
    [TextArea]
    public string toSay;
    public AudioClip clip;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ai = animator.gameObject.GetComponent<AIInterface>();
        textObject = animator.transform.Find("DialogueBox")?.Find("MainText").gameObject;
        canv = textObject.GetComponent<Canvas>();
        if(textObject != null)
        {
            text = textObject.GetComponentInChildren<TextMeshProUGUI>();
            text.text = toSay;
            textObject.SetActive(true);
            LayoutRebuilder.ForceRebuildLayoutImmediate(text.rectTransform);
            if (clip != null)
            {
                AudioSource.PlayClipAtPoint(clip, ai.transform.position, 1);
            }
        }
        else
        {
            Debug.LogError($"missing DialogueBox! on {animator.gameObject.name} in Say State");
        }
        
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ai.RotateTowards(ai.player.transform);    
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        textObject.SetActive(false);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
