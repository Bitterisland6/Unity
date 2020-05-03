using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatUpDown : MonoBehaviour
{
    public AnimationCurve curve;
    Vector3 original;
    // Start is called before the first frame update
    void Start()
    {
        original = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = original + Vector3.up * curve.Evaluate(Time.realtimeSinceStartup);
    }
}
