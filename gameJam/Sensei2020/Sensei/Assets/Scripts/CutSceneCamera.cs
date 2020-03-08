using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneCamera : MonoBehaviour
{
    //variables
    [SerializeField]
    GameObject hitPS;       //particle system spawned on hit
    [SerializeField]
    Transform spawnPlace;   //place of spawn
    [SerializeField]
    LevelLoader ll;         //reference needed to load next scene with transitions

    public void SpawnParticle() {
        Instantiate(hitPS, spawnPlace.position, Quaternion.identity);
    }

    public void LoadScene() {
        ll.LoadNextScene();
    }
}
