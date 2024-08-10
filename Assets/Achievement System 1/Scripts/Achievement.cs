using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Achievement
{
    #region Variables
    public Category category;

    public string title; // Title or name of the achievement
    public string description; // brief explanation of the achievement  
    public Sprite icon; // icon or logo of the achievement
    public int points; // total point, or the ammount point when earn this achievement
    public string[] dependency;
    private bool isUnlock; // default false

    private readonly GameObject visualAchievementPref; // Reference GameObject of this achievement

    private const string playerPrefsPoints = "POINTS";

    // A list of achievement
    private readonly List<Achievement> achievementsDependency = new List<Achievement>();

    // this achievement that id dependent on this achievement
    [HideInInspector]
    public string child;

    #endregion Variables

    public Achievement(string title, string description, Sprite icon, int points, GameObject visualAchievement)
    {
        this.title = title;
        this.description = description;
        this.icon = icon;
        this.points = points;
        this.visualAchievementPref = visualAchievement;

        this.isUnlock = false;

        LoadAchievement();
    }


    public bool UnlockAchievement()
    {
        // Check first if the achievement is lock == false then we can unlock,
        // check if the dependency already unlock the achievement
        if (isUnlock == false && !achievementsDependency.Exists(x => x.isUnlock == false))
        {
            visualAchievementPref.GetComponent<Image>().sprite = AchievementManager.Instance.unlockSprite;
            isUnlock = true;
            SavingAchievement(true);

            if (child != null)
            {
                AchievementManager.Instance.EarnAchievement(child);
            }

            return true;
        }
        else
        {
            // Already earn
            return false;
        }
    }

    private void SavingAchievement(bool value)
    {
        isUnlock = value;

        int tempPoints = PlayerPrefs.GetInt(playerPrefsPoints);

        PlayerPrefs.SetInt(playerPrefsPoints, tempPoints += points);

        PlayerPrefs.SetInt(this.title, value ? 1 : 0); // save bool in memory ( false(0), true(1) )

        PlayerPrefs.Save(); // save when crash or force close

    }
    

    // Called when Creating achievemnt, check if already unlock
    private void LoadAchievement()
    {
        isUnlock = PlayerPrefs.GetInt(this.title) == 1 ? true : false;

        if (isUnlock == true)
        {
            var loadPoint = PlayerPrefs.GetInt(playerPrefsPoints, 0);
            AchievementManager.Instance.pointTMP.text = loadPoint.ToString();

            visualAchievementPref.GetComponent<Image>().sprite = AchievementManager.Instance.unlockSprite;

        }
    }

    public void AddDependency(Achievement dependency)
    {
        achievementsDependency.Add(dependency);
    }
}
