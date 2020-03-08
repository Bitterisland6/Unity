using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScoreTable : MonoBehaviour
{
    //references
    [SerializeField]
    Transform EntryContainer;                   //container for our scores
    [SerializeField]
    Transform EntryTemplate;                    //template for element in container
    [SerializeField]
    TextMeshProUGUI Score;                      //score text
    [SerializeField]
    TextMeshProUGUI Position;                   //position in the table text
    [SerializeField]
    TextMeshProUGUI Name;                       //players name text
    [SerializeField]
    Image EntryBackground;                      //background of an entry in container

    List<Transform> highScoreEntryTransformList;

    //List<HighScoreEntry> tempList;
    
    void Awake() {
        EntryTemplate.gameObject.SetActive(false);

      /*  tempList = new List<HighScoreEntry> {
            new HighScoreEntry{Score = 223, Name = "OLEK"},
            new HighScoreEntry{Score = 321, Name = "OLEK2"},
            new HighScoreEntry{Score = 223, Name = "OLEK3"},
            new HighScoreEntry{Score = 443, Name = "OLEK4"},
        };
        HighScores templ = new HighScores{highScoreEntryList = tempList};

        string jsosn = JsonUtility.ToJson(templ);
        PlayerPrefs.SetString("highScoreTable", jsosn);
        PlayerPrefs.Save();*/
        
        string jsonString = PlayerPrefs.GetString("highScoreTable");
        HighScores highscores = JsonUtility.FromJson<HighScores>(jsonString);
        

        for(int i = 0; i < highscores.highScoreEntryList.Count; i++) {
            for(int j = i; j < highscores.highScoreEntryList.Count; j++) {
                if(highscores.highScoreEntryList[j].Score < highscores.highScoreEntryList[i].Score) {
                    //swapping places
                    HighScoreEntry temp = highscores.highScoreEntryList[i];
                    highscores.highScoreEntryList[i] = highscores.highScoreEntryList[j];
                    highscores.highScoreEntryList[j] = temp;   
                }
            }
        }

        //highscores.highScoreEntryList.RemoveAll((x) => x.Score == 13);
       
        

        highScoreEntryTransformList = new List<Transform>();

        for(int i = 0; i < 11; i++) {
            HighScoreEntry entry = highscores.highScoreEntryList[i];
            CreateHighscoreEntryTransform(entry, EntryContainer, highScoreEntryTransformList);
        }      
    }

    void CreateHighscoreEntryTransform(HighScoreEntry element, Transform container, List<Transform> transformList){
        float templatehight = 30f;
        //spawning element template
        Transform entryTransform = Instantiate(EntryTemplate, container);
        //setting position of the element
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templatehight * (transformList.Count - 1));
        if(transformList.Count != 0)
            entryTransform.gameObject.SetActive(true);

        //setting rank text to a propper string
        int rank = transformList.Count + 1;
        string rankString = "";
        switch(rank) {
            default: 
                    rankString = rank + "TH"; break;
                
            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }
        Position.text = rankString; 

        //filling score texts
        int score = element.Score;
        Score.text = score.ToString();

        //filling name text
        string name = element.Name;
        Name.text = name;  
        
        EntryBackground.enabled = (rank % 2 == 1);
        transformList.Add(entryTransform);
    }

    public static void AddEntry(int score, string name) {
        //creating entry
        HighScoreEntry entry = new HighScoreEntry { Score = score, Name = name};

        //loading saved entries
        string jsonString = PlayerPrefs.GetString("highScoreTable");
        HighScores highscores = JsonUtility.FromJson<HighScores>(jsonString);

        //adding new entry to list
        highscores.highScoreEntryList.Add(entry);
        
        //saving entries
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highScoreTable", json);
        PlayerPrefs.Save();
    }

    class HighScores {
        public List<HighScoreEntry> highScoreEntryList;
    }

    [System.Serializable]
    class HighScoreEntry {
        public int Score;
        public string Name;
    }
}
