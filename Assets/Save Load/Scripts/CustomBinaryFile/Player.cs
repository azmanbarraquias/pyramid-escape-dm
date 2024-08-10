using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public int level = 3;
    public int health = 100;

    #region UI Method

    public TextMeshProUGUI levelTMP;
    public TextMeshProUGUI healthTMP;


    private void Start()
    {
        LoadPlayer();

        levelTMP.text = level.ToString();
        healthTMP.text = health.ToString();
    }

    public void ChangeLevel(int amount)
    {
        level += amount;
        levelTMP.text = level.ToString();
    }

    public void ChangeHealth(int amount)
    {
        health += amount;
        healthTMP.text = health.ToString();
    }

    #endregion UI Method

    public void SavePlayer()
    { 
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData playerData  = SaveSystem.LoadPlayer();
        level = playerData.level;
        levelTMP.text = level.ToString();

        health = playerData.health;
        healthTMP.text = health.ToString();


        Vector3 position = new Vector3(playerData.position[0], playerData.position[1], playerData.position[2]);

        transform.position = position;
    }
}
