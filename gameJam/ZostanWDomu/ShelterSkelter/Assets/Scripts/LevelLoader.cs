using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    Animator transition;
    [SerializeField]
    bool menu;
    [SerializeField]
    bool credits;
    

    void Start() {
        if(menu)
            transition.SetInteger("Menu", 1);
        else if(credits)
            transition.SetInteger("Menu", 2);
        else
            transition.SetInteger("Menu", 0);
    }

    public void LoadLevel(int index) {
        StartCoroutine(Load(index));
    }

    public void LoadMenu() {
        StartCoroutine(Load(0));
    }

    IEnumerator Load(int index) {
        //play anim
        if(menu) 
            transition.SetTrigger("StartMenu");
        else if(credits)
            transition.SetTrigger("StartCredit");
        else
            transition.SetTrigger("Start");
        //wait for anim to end
        yield return new WaitForSeconds(1);
        //load scene
        SceneManager.LoadScene(index);
    }
}
