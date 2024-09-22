using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomManagement : MonoBehaviour
{
    public Button[] rooms;

    void Start()
    {
       int currentLevel =  PlayerPrefs.GetInt(PlayerPrefsID.pyLevel, 1);

        for (int i = 0; i < currentLevel; i++)
        {
            rooms[i].interactable = true;
        }
    }

    public void setLevel (int levl) {
        PlayerPrefs.SetInt(PlayerPrefsID.pyLevel, levl);
    }

    // Update is called once per frame

}
