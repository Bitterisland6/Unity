using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsRiddle : Action
{
    public GameObject[] buttons;
    private int riddle_win = 0;
    private int randomizer;

    //metoda randomLights jest wywoływana jeśli light.ActiveSelf == true;
    //w przeciwnym przypadku riddle_win++ a gdy riddle_win >2 to odtworzyć dźwięk
    //i otworzyć część pokoju
    //czyli pusty obiekt buttons jest interactable i on przy riddle_win
    //działa na ścianę, która podlega metodzie Action_Dissapear
    public override void Action_start()
    {
        randomLigths();
    }

        public void randomLigths()
    {
        System.Random rand = new System.Random();
        foreach(GameObject x in buttons)
        {
            randomizer = rand.Next(0, 2);
            Light light = x.GetComponentInChildren<Light>();
            if (randomizer == 0 && light.gameObject.activeSelf) light.gameObject.SetActive(false);
            else light.gameObject.SetActive(true);
        }
    }
}
