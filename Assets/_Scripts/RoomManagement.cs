using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomManagement : MonoBehaviour
{
    public Button[] rooms;

    public Button pyramidBtn;

    public TextMeshProUGUI PuzzleTMP;

        public TextMeshProUGUI PuzzleTMPScore;


    void Start()
    {
       int currentLevel =  PlayerPrefs.GetInt(PlayerPrefsID.pyLevel, 1);


        for (int i = 0; i < currentLevel; i++)
        {
            if(currentLevel==11)
            {
            pyramidBtn.interactable = true;
           currentLevel = 10;


            }

         PuzzleTMPScore.text = "Room Unlock\n"+currentLevel + " / 10";


            rooms[i].interactable = true;
        }
        if(currentLevel == 10) {
            PuzzleTMP.text = "Now Finish the puzzle!";
        }
    }

    public void setLevel (int levl) {
        PlayerPrefs.SetInt(PlayerPrefsID.pyLevel, levl);
    }

    // Update is called once per frame

}
