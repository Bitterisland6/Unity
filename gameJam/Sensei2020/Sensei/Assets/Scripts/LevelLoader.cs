using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public bool doTransition = false;
    [SerializeField]
    Animator transition;
    [SerializeField]
    float waitTime = 1f;
    int scenesNumber;

    void Start() {
        scenesNumber = SceneManager.sceneCountInBuildSettings;
    }

    void Update() {
        if(doTransition) {
            LoadNextScene();
        }
    }

    public void LoadNextScene() {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex) {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(levelIndex % scenesNumber);
    }
}
