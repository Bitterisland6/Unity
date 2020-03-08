using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollText : MonoBehaviour
{
    public float speed;
    private Vector3 original;
    RectTransform rect;
    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        original = rect.position;
    }

    public void Move()
    {
        if (!IsInvoking("MoveUpdate"))
        {
            InvokeRepeating("UpdateMove", 0.1f, 0.02f);
        }
    }
    public void Reset()
    {
        CancelInvoke("UpdateMove");
        rect.position = original;
    }
    private void UpdateMove()
    {
        rect.position += Vector3.right * speed;
    }
}
