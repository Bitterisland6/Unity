using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public LevelLoader loader;
    public void EndGame(bool win)
    {
        int pts = Bank.Instance().points;
        PlayerPrefs.SetInt("win", win ? 1 : 0);
        PlayerPrefs.SetInt("score", pts);
        int hs;
        if(PlayerPrefs.HasKey("highScore"))
        {
            hs = PlayerPrefs.GetInt("highScore");
        }
        else
        {
            hs = 0;
        }
        if(win)
        {
            PlayerPrefs.SetInt("highScore", pts > hs ? pts : hs);
        }
        
        PlayerPrefs.Save();
        loader.LoadLevel(3);
    }
}
