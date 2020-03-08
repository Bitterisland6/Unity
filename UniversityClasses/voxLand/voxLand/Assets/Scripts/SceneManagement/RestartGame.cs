using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    [SerializeField]
    float restartTimeOffset;
    bool restart;
    float restartTime;
    // Start is called before the first frame update
    void Start() {
        restart = false;
    }

    // Update is called once per frame
    void Update() {
        if(restart && restartTime < Time.unscaledTime){
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void Restart(){
        restart = true;
        restartTime = Time.time + restartTimeOffset;
    }
}
