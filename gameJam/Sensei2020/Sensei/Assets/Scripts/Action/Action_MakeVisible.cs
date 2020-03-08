using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_MakeVisible : MonoBehaviour
{
    public GameObject wall;
    private GameObject m_player;
    private void Start()
    {
        m_player = GameObject.FindWithTag("Player");
    }

   void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered");
        if (other.CompareTag("Player"))
        {
            wall.SetActive(true);
        }
    }
 
}
