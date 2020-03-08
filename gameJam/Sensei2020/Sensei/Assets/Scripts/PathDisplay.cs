using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDisplay : MonoBehaviour
{
    public GameObject[] tilesArray;
    public Material highlightMaterial;
    public Material defaultMaterial;

    int arrIndex;

    void Start() 
    {
        StartHighLight();    
    }
    void StartHighLight() 
    {
        InvokeRepeating("HighlightTile", 1, 0.07f);
    }

    void ResetHighlight()
    {
        InvokeRepeating("ResetTile", 1, 0.07f);
    }

    void ResetTile()
    {
        tilesArray[arrIndex].GetComponent<Renderer>().material = defaultMaterial;
        ++arrIndex;
        if (arrIndex >= tilesArray.Length)
        {
            arrIndex = 0;
            CancelInvoke();
            StartHighLight();
        }
    }
    

    void HighlightTile() 
    {
        tilesArray[arrIndex].GetComponent<Renderer>().material = highlightMaterial;
        ++arrIndex;
        if (arrIndex >= tilesArray.Length)
        {
            arrIndex = 0;
            CancelInvoke();
            ResetHighlight();
        }
    }
}
