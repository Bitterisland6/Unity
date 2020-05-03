using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatAbove : MonoBehaviour
{
    public AnimationCurve heightReaction;
    private void FixedUpdate()
    {
        RaycastHit h;
        if (Physics.Raycast(transform.position + Vector3.up * 20, Vector3.down, out h))
        {
            float height = transform.position.y - h.point.y;
            transform.position += Vector3.up * heightReaction.Evaluate(height);
        }
    }
}
