using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomManagement : MonoBehaviour
{
    public Button[] rooms;

    public int currentLevel = 1;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < currentLevel; i++)
        {
            rooms[i].interactable = true;
        }
    }

    // Update is called once per frame

}
