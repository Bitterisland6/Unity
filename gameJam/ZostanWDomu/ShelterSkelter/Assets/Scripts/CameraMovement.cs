using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : GameEventListener {
    //variables
    Camera mycam;
   
    Vector3 translation;
    TileMap map;
    [SerializeField]
    float panSpeed = 10f;
    [SerializeField]
    float panBorder = 2f;
    [SerializeField]
    float scrollSpeed = 20f;
    [SerializeField]
    bool isMenu;
   
    [SerializeField]
    AnimationCurve zoomCurve;
    float minZoom = 1f;
    float maxZoom = 0f;
    float zoom = 0.5f;
   
    float scroll;
    [SerializeField]
    Vector2 panLimitMax;
    [SerializeField]
    Vector2 panLimitMin;
    Vector3 startPoint = new Vector3(63f, 0f, 63f);
    Vector3 offset;
    
    [SerializeField]
    AnimationCurve distCurve;
    float minDist = 0f;
    float maxDist = 130f;
    float dist;
    Vector3 destination;
    
    int playerX;
    int playerY;

    bool dragging;
    Vector3 startPosition;
    Vector3 currentPosition;
    Vector3 startMouse;
    Vector3 direction;

    void Start () {
        mycam = Camera.main;
        offset = mycam.transform.position - startPoint;
        map = gameObject.GetComponent<TileMap>();
        if(!isMenu) {
            playerX = map.startX;
            playerY = map.startY;
            mycam.transform.position = new Vector3(playerX * map.TileSize, 0f, playerY * map.TileSize) + offset;
        } else {
            mycam.transform.position = new Vector3(map.Columns / 2 * map.TileSize, 0f, map.Rows / 2 * map.TileSize) + offset;
        }
        action.AddListener(Focus);
    }

	void Update() {
        if(!isMenu) {
            translation = mycam.transform.position;
            translation = Quaternion.Euler(0f, -45f, 0f) * translation;
            if(Input.mousePosition.x >= Screen.width - panBorder || Input.GetKey(KeyCode.RightArrow))
                translation.x += panSpeed * Time.deltaTime * (zoomCurve.Evaluate(zoom) / zoomCurve.Evaluate(minZoom)) * 10f;
            if(Input.mousePosition.x <= panBorder || Input.GetKey(KeyCode.LeftArrow))
                translation.x -= panSpeed * Time.deltaTime * (zoomCurve.Evaluate(zoom) / zoomCurve.Evaluate(minZoom)) * 10f;
            if(Input.mousePosition.y >= Screen.height - panBorder || Input.GetKey(KeyCode.UpArrow))
                translation.z += panSpeed * Time.deltaTime * (zoomCurve.Evaluate(zoom) / zoomCurve.Evaluate(minZoom)) * 10f;
            if(Input.mousePosition.y <= panBorder || Input.GetKey(KeyCode.DownArrow))
                translation.z -= panSpeed * Time.deltaTime * (zoomCurve.Evaluate(zoom) / zoomCurve.Evaluate(minZoom)) * 10f;
        
            scroll = Input.GetAxis("Mouse ScrollWheel");
            zoom -= scroll * scrollSpeed * Time.deltaTime;

            if(Input.GetMouseButtonDown(2)) {
                startPosition = mycam.transform.position;
                startMouse = Input.mousePosition;
            }

            if(Input.GetMouseButton(2)) {
                direction.x = startMouse.x - Input.mousePosition.x;
                direction.y = 0f;
                direction.z = startMouse.y - Input.mousePosition.y;
                direction.z = direction.z * 3.56381818f * 0.75f * mycam.orthographicSize / Screen.height;
                direction.x = direction.x * 3.56381818f * mycam.orthographicSize / Screen.width;
                direction = Quaternion.Euler(0f, 45f, 0f) * direction;
                if(!dragging) {
                    dragging = true;
                }
            }
            else {
                dragging = false;
            }
            
            if(dragging) {
                mycam.transform.position = startPosition + direction;
            } else {
                zoom = Mathf.Clamp(zoom, maxZoom, minZoom);
                translation = Quaternion.Euler(0f, 45f, 0f) * translation;

                mycam.transform.position = translation;
                mycam.transform.position = new Vector3(Mathf.Clamp(mycam.transform.position.x, panLimitMin.x, panLimitMax.x),
                                                        mycam.transform.position.y,
                                                        Mathf.Clamp(mycam.transform.position.z, panLimitMin.y, panLimitMax.y));
                mycam.orthographicSize = zoomCurve.Evaluate(zoom);
            }
        }
    }

    private void FocusOnPlayer() {
        dist = Vector3.Distance(mycam.transform.position, destination);
        dist = Mathf.Clamp(dist, minDist, maxDist);
        if(dist < 0.5f)
            CancelInvoke("FocusOnPlayer");
        mycam.transform.position =  Vector3.MoveTowards(mycam.transform.position, destination, distCurve.Evaluate(dist));
    }

    public void Focus(){
        if(PlayerGroup.Instance().CurrentPlayer() == null) {
            return;
        }
        else {
            CancelInvoke("FocusOnPlayer");
            playerX = PlayerGroup.Instance().CurrentPlayer().pos.x;
            playerY = PlayerGroup.Instance().CurrentPlayer().pos.y;
            destination = new Vector3(playerX * map.TileSize, 0f, playerY * map.TileSize) + offset;
            InvokeRepeating("FocusOnPlayer", 0.0f, 0.02f);
        }
    }

    public void ChangeTranslation(Vector2Int dir) {
        translation = mycam.transform.position;
        translation = Quaternion.Euler(0f, -45f, 0f) * translation;
        translation.x += dir.x * panSpeed * Time.deltaTime * (zoomCurve.Evaluate(zoom) / zoomCurve.Evaluate(minZoom)) * 10f;
        translation.z += dir.y * panSpeed * Time.deltaTime * (zoomCurve.Evaluate(zoom) / zoomCurve.Evaluate(minZoom)) * 10f;
        translation = Quaternion.Euler(0f, 45f, 0f) * translation;
        mycam.transform.position = translation;
        mycam.transform.position = new Vector3(Mathf.Clamp(mycam.transform.position.x, panLimitMin.x, panLimitMax.x),
                                                mycam.transform.position.y,
                                                Mathf.Clamp(mycam.transform.position.z, panLimitMin.y, panLimitMax.y));
    }

    public void ChangeZoom(float diff) {
        zoom -= diff * scrollSpeed * Time.deltaTime;
        zoom = Mathf.Clamp(zoom, maxZoom, minZoom);
        mycam.orthographicSize = zoomCurve.Evaluate(zoom);
    }
}
