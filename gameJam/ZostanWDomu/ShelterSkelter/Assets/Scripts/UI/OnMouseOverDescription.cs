using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnMouseOverDescription : MonoBehaviour
{
    [TextArea]
    public string description;
    public GameObject textBox;
    public float showDelay;
    public float disapearDelay;
    private GameObject o;
    private GameObject mainCanvas;
    private Vector3 mouse;
    // Start is called before the first frame update
    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "Game")
        {
            if (mainCanvas == null)
                mainCanvas = GameObject.Find("Canvas");
            if (o == null)
                o = GameObject.Find("mouseOVER");
        }
    }
    // Update is called once per frame
    private void Show()
    {
        if (o == null || mainCanvas == null) return;
        if(!o.activeSelf)
        {
            o.GetComponentInChildren<TMPro.TextMeshProUGUI>().SetText(description);
            o.SetActive(true);
            o.GetComponent<RectTransform>().position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            
        }
    }
    private void UnShow()
    {
        if (o == null || mainCanvas == null) return;
        o.SetActive(false);
    }
    private void OnMouseOver()
    {
        if (o == null || mainCanvas == null) return;
        o.GetComponent<RectTransform>().position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        o.GetComponentInChildren<TMPro.TextMeshProUGUI>().SetText(description);
    }
    private void OnMouseEnter()
    {
        Show();
        /*
        Vector3 mouseLast = mouse;
        mouse = Input.mousePosition;
        if(mouse.x == mouseLast.x && mouse.y == mouseLast.y)
        {
            if(!IsInvoking("Show") && o == null)
            {
                CancelInvoke("UnShow");
                Invoke("Show", showDelay);
            }
        }
        else
        {
            CancelInvoke("Show");
            Invoke("UnShow", disapearDelay);
        }
        */
    }
    private void OnMouseExit()
    {
        UnShow();
        /*
        CancelInvoke("Show");
        Invoke("UnShow", disapearDelay);
        */
    }
    private void OnDestroy()
    {
        if (o == null || mainCanvas == null) return;
        o.SetActive(false);
    }
}
