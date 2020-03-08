using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WSButton : MonoBehaviour
{
    public Image image;
    public Color mouseOver;
    public Color normal;
    public UnityEvent onClick;
    private void OnMouseDown()
    {
        onClick.Invoke();
    }
    private void OnMouseEnter()
    {
        image.color = mouseOver;
    }
    private void OnMouseExit()
    {
        image.color = normal;
    }
}
