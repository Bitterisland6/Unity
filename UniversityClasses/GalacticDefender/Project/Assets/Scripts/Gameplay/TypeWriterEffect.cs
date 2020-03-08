using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeWriterEffect : MonoBehaviour
{
    //variables
    [SerializeField]
    float Delay;            //delay in showing letters
    [SerializeField]
    string Text;            //text to display
    
    //private variables
    string currentText;
    bool started;
    
    // Start is called before the first frame update
    void Awake() {
        //initializing variables
        currentText = "";
        started = false;
    }

    //enumerator making type writing effect
    IEnumerator ShowText() {
        for(int i = 0; i <= Text.Length; i++) {
            //getting consecutive prefixes of given text, and displaying them
            currentText = Text.Substring(0, i);
            gameObject.GetComponent<TextMeshProUGUI>().text = currentText;
            //waiting for next move
            yield return new WaitForSecondsRealtime(Delay);
        }
    }

    //function activating writing
    public void StartWriting() {
        //starting couroutine if it hasn't been started yet
        if(!started) {
            StartCoroutine(ShowText());
            started = !started;
        }
    }
}
