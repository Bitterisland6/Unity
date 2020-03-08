using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverSpawn : MonoBehaviour
{
    [SerializeField]
    GameObject SmallCoverPreFab; //prefab for small asteroid
    [SerializeField]
    GameObject BigCoverPreFab; //prefab for big asteroid
    [SerializeField]
    Vector3 [] spawnPlaces; //places to spawn covers

    void Awake() {
        spawnCovers();
    }

    //function spawning covers on right places
    public void spawnCovers() {
        for(int i = 0; i < 4; i++) {
            if(i < 2) {
                Instantiate(SmallCoverPreFab, spawnPlaces[i], Quaternion.identity);
            }
            else {
                Instantiate(BigCoverPreFab, spawnPlaces[i], Quaternion.identity);
            }
        }
    }
}
