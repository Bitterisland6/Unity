using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    //variables
    [SerializeField]
    int EnemiesRow;             //number of enemies in a row
    [SerializeField]
    int EnemiesColumn;          //number of enemies in a collumn
    [SerializeField]
    GameObject enemyPreFab;     //enemy, that will spawn
    [SerializeField]
    Transform SpawnPoint;       //begining of spawn


    void Awake() {
        spawn();
        //displaying number of enemies
        EnemiesTextController.Enemies += EnemiesColumn*EnemiesRow;
    }

    public void spawn() {
        for(int i = 0; i < EnemiesColumn; i++) {
            for(int j = 0; j < EnemiesRow; j++) {
                Instantiate(enemyPreFab, SpawnPoint.position + new Vector3(j*3.0f, i*3.0f, 0f), Quaternion.identity);
            }
        }
    }
}
