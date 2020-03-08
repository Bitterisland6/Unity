using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneManager : MonoBehaviour
{
    //variables
    [SerializeField]
    GameObject poofFX;
    [SerializeField]
    GameObject camera;
    [SerializeField]
    LevelLoader ll;


    void Start() {
        
    }

    void Update() {
        
    }

    public void Spawn() {
        Instantiate(poofFX, camera.transform.position - Vector3.right, Quaternion.Euler(90f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
        Instantiate(poofFX, camera.transform.position, Quaternion.Euler(90f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
    }

    public void GoToMenu(){
        ll.LoadNextScene();
    }
}
