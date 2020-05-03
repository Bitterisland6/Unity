using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePosition : MonoBehaviour
{
    private Vector3 originalPos;
    public float speed;
    public Vector3 moveVec;
    private Vector3 target;
    private RectTransform trans;
    private void Start()
    {
        trans = GetComponent<RectTransform>();
        originalPos = trans.position;
        
    }
    public void Move()
    {

        target = originalPos + moveVec * Screen.width / 1920;
        CancelInvoke("Movement");
        InvokeRepeating("Movement", 0, 0.02f);
    }
    public void Back()
    {
        target = originalPos;
        CancelInvoke("Movement");
        InvokeRepeating("Movement", 0, 0.02f);
    }
    private void Movement()
    {
        transform.position = Vector3.MoveTowards(trans.position, target, speed * Screen.width / 1920);
        if (Vector3.Distance(target, trans.position) < 0.1f)
        {
            CancelInvoke("Movement");
        }
    }
}
