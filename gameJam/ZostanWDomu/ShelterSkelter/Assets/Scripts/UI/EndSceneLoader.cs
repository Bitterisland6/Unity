using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EndSceneLoader : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI highScore;
    public GameObject win;
    public GameObject lose;
    public GameObject newHS;

    [SerializeField]
    bool isEndscene;

    // Start is called before the first frame update
    void Start()
    {
        win.SetActive(false);
        lose.SetActive(false);
        newHS.SetActive(false);

        int sc = PlayerPrefs.GetInt("score");
        int hs = PlayerPrefs.GetInt("highScore");
        int w = PlayerPrefs.GetInt("win");
        if(w == 1)
        {
            //player won
            win.SetActive(true);
            score.text = sc.ToString();
            highScore.text = hs.ToString();
            if(sc == hs)
            {
                newHS.SetActive(true);
            }
        }
        else
        {
            //lose
            lose.SetActive(true);
            score.text = sc.ToString();
            highScore.text = hs.ToString();
        }
        if(isEndscene) {
            newHS.SetActive(true);
        }
    }
}
