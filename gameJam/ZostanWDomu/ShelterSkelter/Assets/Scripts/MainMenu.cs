using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    float moveDuration;
    [SerializeField]
    Vector2Int[] directions;
    [SerializeField]
    LevelLoader loader;
    
    //mprivate variables
    CameraMovement camera;
    float startMove;
    int zoomDir;
    float zoom;
    int dirIndex;
    int tempIndex;
    
    bool startZoom;

    // Start is called before the first frame update
    void Start() {
        camera = GetComponent<CameraMovement>();
        startMove = 0f;
        zoomDir = 1;
        dirIndex = Random.Range(0, directions.Length);
        tempIndex = dirIndex;
    }

    // Update is called once per frame
    void FixedUpdate() {
        if(!startZoom) {
            camera.ChangeZoom(-0.5f);
            startZoom = true;
        }
        
        camera.ChangeTranslation(directions[dirIndex]);

        if(startMove > moveDuration) {
            startMove = 0;
            //setting direction for camera
            tempIndex = Random.Range(0, directions.Length);
            if(tempIndex == dirIndex) {
                dirIndex = (tempIndex + 2) % directions.Length;
                tempIndex = dirIndex;
            } else  if(tempIndex == dirIndex + 1) {
                dirIndex = (tempIndex + 1) % directions.Length;
                tempIndex = dirIndex;
            } else
                dirIndex = tempIndex;
            //setting zoom
            zoom = Random.Range(-2, 2);
            zoomDir *= -1;
            camera.ChangeZoom(zoom * zoomDir);
            
        }
        startMove += Time.deltaTime;
    }

    public void PlayGame() {
        loader.LoadLevel(1);
    }

    public void Credits() {
        loader.LoadLevel(2);
    }

    public void Menu() {
        loader.LoadLevel(0);
    }

    public void Tutorial() {
        loader.LoadLevel(4);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
