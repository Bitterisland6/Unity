using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileMap : MonoBehaviour
{
    //variables
    [SerializeField]
    public int Rows = 10;               //number of rows
    [SerializeField]
    public int Columns = 20;            //number of columns
    [SerializeField]
    public float TileSize = 1;          //size of one tile
    [SerializeField]
    int CentreRadius = 3;               //radius of city centre area
    [SerializeField]
    int  SuburbsRadius = 5;             //radius of suburbs area
    [SerializeField]
    GameObject[] UrbanPrefabs;          //prefabs of urban area
    [SerializeField]
    GameObject[] SuburbPrefabs;         //prefabs of suburban area
    [SerializeField]
    GameObject[] AgroPrefabs;           //prefabs of agro area
    [SerializeField]
    GameObject InvisiblePrefab;         //prefabs of undiscovered tiles
    [SerializeField]
    bool isMenu;                        //flag saying if tilemap is used in main menu or in game

    public Tile[,] tileMap;             //map holding tiles

    //private variables
    static int[,] gameMap;              //game map made of tiles, holds numbers indicating which are is tile from on given position
    static bool[,] helpMap;             //map with discovered tiles
    static int middleX;                 //coordinate x of the middle point of the map
    static int middleY;                 //coordinate y of the middle point of the map
    float distance;                     //distance of some tile form middle point
    int prefabIndex;                    //indef of prefab in array of prefabs
    [HideInInspector]
    public int startX;                  //coordinate x of starting position
    [HideInInspector]
    public int startY;                  //coordinate y of starting position
    GameObject ReturnedObj;             //game object used to catch GameObject returned by instantiate

    static TileMap instance;

    void Awake() {
        //variables initialization
        gameMap = new int[Columns, Rows];
        helpMap = new bool[Columns, Rows];
        tileMap = new Tile[Columns, Rows];
        middleX = Columns / 2;
        middleY = Rows / 2;
        distance = 0;
        startX = Random.Range(0, Columns);
        startY = Random.Range(0, Rows);
        instance = this;
    }
    private void Start()
    {
    //filling map with tiles
    FillMap();
        
        //showing undiscovered part of map
        if(!isMenu){
            StartMap();
            //spawning map
            PlayerMoved(startX, startY);
            foreach(Player p in PlayerGroup.Instance().players)
            {
                if(p.inGame)
                {
                    p.pos = new Vector2Int(startX, startY);
                    tileMap[startX, startY].OnPlayerEnter.Invoke();
                }
            }
        } else
            ShowMap();
        
       
    }

    //function filling maps with tiles from right area
    void FillMap() {
        for(int y = 0; y < Rows; y++) {
            for(int x = 0; x < Columns; x++) {
                gameMap[x, y] = AreaIndex(x, y);
            }
        }
    }

    void StartMap() {
        for(int y = 0; y < Rows; y++) {
            for(int x = 0; x < Columns; x++) {
                if(helpMap[x, y] == false) {
                    ReturnedObj = Instantiate(InvisiblePrefab, new Vector3(x * TileSize, 0.0f, y * TileSize), Quaternion.identity);
                    tileMap[x, y] = ReturnedObj.GetComponent<Tile>();
                    tileMap[x, y].pos = new Vector2Int(x, y);
                }
            }
        }
    }

    //function updating map based on player position (x, y) - showing undiscovered tiles that are incident to (x,y) tile
    public void PlayerMoved(int x, int y) {
        for(int xt = x - 1; xt <= x + 1; xt++) {
            if(xt >= 0 && xt < Columns) {  //checking if x is in map bounds 
                for(int yt = y - 1; yt <= y + 1; yt++) {
                    if(yt >= 0 && yt < Rows) {    //checking if y is in map bounds
                        SpawnMap(xt, yt);
                    }
                }   
            }
        }
    }

    public void SpawnMap(int x, int y, GameObject prefab=null) {         
        if(helpMap[x, y] == false) {    //tile has not been shown yet
            Tile temp = tileMap[x, y];
            tileMap[x, y] = null;
            Destroy(temp.gameObject);
            if(prefab) {               //spawning given prefab
                ReturnedObj = Instantiate(prefab, new Vector3(x * TileSize, 0.0f, y * TileSize), Quaternion.identity);
            } else {  //spawning random prefab from right area
                if(gameMap[x, y] == 0) {    //urban
                    prefabIndex = Random.Range(0, UrbanPrefabs.Length);
                    ReturnedObj = Instantiate(UrbanPrefabs[prefabIndex], new Vector3(x * TileSize, 0.0f, y * TileSize), Quaternion.identity); 
                } else if(gameMap[x, y] == 1) { //suburb
                    prefabIndex = Random.Range(0, SuburbPrefabs.Length);
                    ReturnedObj = Instantiate(SuburbPrefabs[prefabIndex], new Vector3(x * TileSize, 0.0f, y * TileSize), Quaternion.identity); 
                } else {    //agro
                    prefabIndex = Random.Range(0, AgroPrefabs.Length);
                    ReturnedObj = Instantiate(AgroPrefabs[prefabIndex], new Vector3(x * TileSize, 0.0f, y * TileSize), Quaternion.identity); 
                }
            }
            
            helpMap[x,y] = true;    //tile (x,y) has been discovered
            tileMap[x, y] = ReturnedObj.GetComponent<Tile>();
            tileMap[x,y].pos = new Vector2Int(x, y);
        }
    }

    public void ShowMap() {
        for(int x = 0; x < Columns; x++) {
            for(int y = 0; y < Rows; y++) {
                    if(gameMap[x, y] == 0) {    //urban
                        prefabIndex = Random.Range(0, UrbanPrefabs.Length);
                        ReturnedObj = Instantiate(UrbanPrefabs[prefabIndex], new Vector3(x * TileSize, 0.0f, y * TileSize), Quaternion.identity); 
                    } else if(gameMap[x, y] == 1) { //suburb
                        prefabIndex = Random.Range(0, SuburbPrefabs.Length);
                        ReturnedObj = Instantiate(SuburbPrefabs[prefabIndex], new Vector3(x * TileSize, 0.0f, y * TileSize), Quaternion.identity); 
                    } else {    //agro
                        prefabIndex = Random.Range(0, AgroPrefabs.Length);
                        ReturnedObj = Instantiate(AgroPrefabs[prefabIndex], new Vector3(x * TileSize, 0.0f, y * TileSize), Quaternion.identity); 
                    }
            }
        }
    }

    //function checking in which area is tile (x,y)
    private int AreaIndex(int x, int y) {
        //counting distance from the middle point
        distance = Mathf.Sqrt((x - middleX) * (x - middleX) + (y - middleY) * ( y - middleY));
        //checking in which area is given tile
        if(distance < CentreRadius)         //city centre
            return 0;
        else if(distance < SuburbsRadius)   //suburbs
            return 1;
        else                                //agro
            return 2;
    }

    public static TileMap Instance() {
        if(instance == null) {
            Debug.LogError("TileMap doesn't exists");
        }
        return instance;
    }
}
