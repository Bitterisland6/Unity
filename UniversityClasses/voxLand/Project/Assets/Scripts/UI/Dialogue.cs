using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;
public class Dialogue : MonoBehaviour
{
    public GameObject textObject;
    private TextMeshProUGUI text;
    public DialogueNode root;
    private DialogueNode current;
    // Start is called before the first frame update
    void Start()
    {
        text = textObject.GetComponentInChildren<TextMeshProUGUI>();
        current = root;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Choice(int i)
    {
        current.options[i].action.ActionStart();
        current = current.options[i].transition;
        text.text = ToString();
    }
    public void StartDialogue()
    {
        textObject.SetActive(true);
        text.text = ToString();
    }
    public void PauseDialogue()
    {
        textObject.SetActive(false);
    }
    public void EndDialogue()
    {
        textObject.SetActive(false);
        current = root;
    }
    public override string ToString()
    {
        StringBuilder str = new StringBuilder(current.npcText);
        for(int i = 0; i < current.options.Length; i++)
        {
            str.Append($"\n{i+1}. {current.options[i].optionText}");
            if(current.options[i].action != null)
            {
                str.Append($"{current.options[i].action.label}");
            }
        }
        return str.ToString();
    }
}
[System.Serializable]
public class DialogueNode
{
    [TextArea] public string npcText;
    public DialogueOption[] options;
}
[System.Serializable]
public class DialogueOption
{
    [TextArea] public string optionText;
    public DialogueNode transition;
    public Action action;
}

