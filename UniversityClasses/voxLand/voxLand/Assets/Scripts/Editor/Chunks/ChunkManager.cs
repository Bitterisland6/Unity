using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    public int chunkLoadRange;
    public int chunkDeLoadDistance;
    public int chunkSize;
    public float refreshInterval;
    public float refreshSpeed;
    public Transform world;
    public Vector3 onStartOffSet;
    [Range(0, 1)]
    public float elevationSpeed;

    private Dictionary<Vector2Int, GameObject> loadedChunks = new Dictionary<Vector2Int, GameObject>(); 
    void Start()
    {
        StartCoroutine("DoRefresh");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("bah");
            ForceChunkRefresh();
        }
    }
    void NewChunk(Vector2Int position)
    {
        Debug.DrawLine(new Vector3(position.x, 30, position.y), new Vector3(position.x, 0, position.y), Color.red, 3.0f);
        if(!loadedChunks.ContainsKey(position))
        {
            GameObject obj = Resources.Load(string.Format("Chunks/chunk_x{0}z{1}", position.x, position.y))as GameObject;
            if (obj != null)
            {
                GameObject instance = Instantiate(obj, world);
                Chunk chunk = instance.GetComponent<Chunk>();
                chunk.manager = this;
                chunk.chunkKey = position;
                chunk.onStartOffSet = onStartOffSet;
                chunk.elevationSpeed = elevationSpeed;

                loadedChunks.Add(position, obj);
            }
            else
            {
                //Temporary place (this shouldnt be placed in game code as it is pure editor feature)
                //CreateBlankChunk(position);
            }
        }
    }
    IEnumerator DoRefresh()
    {
        for(; ; )
        {
            StartCoroutine("RefreshChunks");
            yield return new WaitForSecondsRealtime(refreshInterval);
        }
    }
    IEnumerator RefreshChunks()
    {
        Vector2Int position = new Vector2Int((((int)player.position.x) / chunkSize) * chunkSize, (((int)player.position.z) / chunkSize) * chunkSize);
        for (int x = position.x - chunkLoadRange; x <= position.x + chunkLoadRange; x += chunkSize)
        {
            for (int z = position.y - chunkLoadRange; z <= position.y + chunkLoadRange; z += chunkSize)
            {
                NewChunk(new Vector2Int(x, z));
                yield return new WaitForSecondsRealtime(refreshSpeed);
            }
        }
    }
    void ForceChunkRefresh()
    {
        Vector2Int position = new Vector2Int((((int)player.position.x) / chunkSize) * chunkSize, (((int)player.position.z) / chunkSize) * chunkSize);
        for (int x = position.x - chunkLoadRange; x <= position.x + chunkLoadRange; x += chunkSize)
        {
            for (int z = position.y - chunkLoadRange; z <= position.y + chunkLoadRange; z += chunkSize)
            {
                NewChunk(new Vector2Int(x, z));
            }
        }
    }
    public void deloadCall(Vector2Int position)
    {
        loadedChunks.Remove(position);
    }
    void CreateBlankChunk(Vector2Int position)
    {
        string name = string.Format("chunk_x{0}z{1}", position.x, position.y);
        GameObject chunk = new GameObject(name);
        chunk.AddComponent<Chunk>();
        chunk.transform.position = new Vector3(position.x, 0, position.y);
        Debug.Log(PrefabUtility.SaveAsPrefabAsset(chunk, "Assets/Resources/Chunks/"+name+".prefab"));
        Destroy(chunk);
    }
}
