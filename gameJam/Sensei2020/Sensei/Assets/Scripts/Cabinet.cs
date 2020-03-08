using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabinet : MonoBehaviour
{
    public GameObject colectible;
    public GameObject questionMark;

    // Start is called before the first frame update
    public void UpdateCabinet()
    {
        Collectible col  = colectible.GetComponent<Collectible>();
        
        PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if(player.colected.ContainsKey(col.GetName()))
        {
            Destroy(questionMark);
            colectible = Instantiate(colectible, transform.position + Vector3.up, Quaternion.identity, transform);
            colectible.tag = "Untagged";
            colectible.layer = LayerMask.NameToLayer("Default");
            Destroy(colectible.GetComponent<Collectible>());
        }
    }
}
