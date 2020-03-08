using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    // Start is called before the first frame update
    public ChunkManager manager;
    public Vector2Int chunkKey;
    private Vector3 originalPosition;
    public Vector3 onStartOffSet;
    public float elevationSpeed;
    void Start()
    {
        InvokeRepeating("Refresh", 0.0f, 1.0f);
        originalPosition = transform.position;
        transform.position = transform.position + onStartOffSet;
        StartCoroutine("ElevateChunk");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator ElevateChunk()
    {
        while(originalPosition.y > transform.position.y + 0.2)
        {
            transform.position = Vector3.Lerp(transform.position, originalPosition, elevationSpeed);
            yield return new WaitForEndOfFrame();
        }
        transform.position = originalPosition;
    }
    IEnumerator DelevateChunk()
    {
        while (gameObject != null)
        {
            transform.position = Vector3.Lerp(transform.position, originalPosition - Vector3.up*20, elevationSpeed);
            yield return new WaitForEndOfFrame();
        }
    }
    void Refresh()
    {
        if((chunkKey - new Vector2(manager.player.position.x, manager.player.position.z)).sqrMagnitude >
            (manager.chunkDeLoadDistance) * (manager.chunkDeLoadDistance))
        {
            manager.deloadCall(chunkKey);
            StartCoroutine("DelevateChunk");
            Destroy(gameObject, 3.0f);
        }
    }
}
