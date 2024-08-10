using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{
    #region Variable 
    [Header("Achievement Manager")]
    public GameObject achievementMenu;

    [Header("Achievement Prefabs")]
    public GameObject achievementTemplate;
    public GameObject earnAchievementTemplate;

    [Header("UI Setup")]
    public ScrollRect scrollRect;
    public Sprite unlockSprite;
    public TextMeshProUGUI pointTMP;

    [Header("Achievement Holder")]
    public GameObject generalList;
    public GameObject otherList;

    [Header("Earning Achievement")]
    public Transform notificationHolder;
    public AudioSource audioSource;
    public AudioClip unlockAchievementSound;
    public float fadeAnimationSpeed = 1f;

    //Constant
    private const string playerPrefsPoints = "POINTS";

    public Dictionary<string, Achievement> achievements = new Dictionary<string, Achievement>();

    [Header("Create Achievement")] [Tooltip("Enter number achievement you want to add")]
    public Achievement[] achievement;

    #endregion Variable

    #region Achievement Manager Singleton
    private static AchievementManager instance;
    public static AchievementManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AchievementManager>();
            }
            return instance;
        }
    }
    #endregion Achievement Manager Singleton

    private void Start()
    {
        SelectGeneral();

        achievementMenu.SetActive(true);

        GenerateAchievement();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            OpenCloseAchievement();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EarnAchievement("Press 1");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EarnAchievement("Press 2");
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            EarnAchievement("Press O");
        }
    }

    public void OpenCloseAchievement()
    {
        achievementMenu.SetActive(!achievementMenu.activeSelf);
    }

    #region Create and Set Achievement

    public void GenerateAchievement()
    {
        foreach (var achievementItem in achievement)
        {
            CreateAchievement(GetCategoryParent(achievementItem.category), achievementItem.title, achievementItem.description,
                achievementItem.icon, achievementItem.points, achievementItem.dependency);
        }

        // Creating achievement via code,

        //CreateAchievement(generalList.transform, "My Title 0", "My Description", sprite, 10);

        //CreateAchievement(generalList.transform, "My Title 1 and 2",
        //    "My Title 1 and 2", sprite, 30, new string[] { "My Title 0", "My Title 1" });
    }

    public void CreateAchievement(Transform parent, string title, string description, Sprite icon, int point, string[] dependencies = null)
    {
        var achievementPref = Instantiate(achievementTemplate);
        achievementPref.transform.localScale = new Vector3(1, 1, 1);

        Achievement newAchievement = new Achievement(title, description, icon, point, achievementPref);

        achievements.Add(title, newAchievement);

        SetAchievementInfo(parent, achievementPref, title);

        // dependencies
        if (dependencies != null)
        {
            foreach (string achievementTitle in dependencies)
            {
                Achievement dependency = achievements[achievementTitle];
                dependency.child = title;
                newAchievement.AddDependency(dependency);

                // Dependency = Press Space - not depend on anything <- child = Press W
                // NewAchiivement = Press W --> depend on Press Space
            }
        }
    }

    public void SetAchievementInfo(Transform parent, GameObject achievementPref, string title)
    {
        achievementPref.transform.SetParent(parent);
        achievementPref.transform.localScale = new Vector3(1, 1, 1);

        achievementPref.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = achievements[title].title;
        achievementPref.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = achievements[title].description;
        achievementPref.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = achievements[title].icon;
        achievementPref.transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = achievements[title].points.ToString();
    }
    #endregion

    #region Change Category
    public void ChangeCategory(Category category)
    {
        if (category == Category.General)
        {
            generalList.SetActive(true);
            otherList.SetActive(false);
            scrollRect.content = generalList.GetComponent<RectTransform>();
        }
        else if (category == Category.Other)
        {
            generalList.SetActive(false);
            otherList.SetActive(true);
            scrollRect.content = otherList.GetComponent<RectTransform>();
        }
        else
        {
            generalList.SetActive(false);
            otherList.SetActive(false);
        }
    }

    public void SelectGeneral()
    {
        ChangeCategory(Category.General);
    }

    public void SelectOther()
    {
        ChangeCategory(Category.Other);
    }

    #endregion Change Category

    #region Earn Achievement
    public void EarnAchievement(string title)
    {
        if (achievements[title].UnlockAchievement() == true)
        {
            audioSource.PlayOneShot(unlockAchievementSound);

            GameObject newEarnAchievement = Instantiate(earnAchievementTemplate);

            SetAchievementInfo(notificationHolder, newEarnAchievement, title);

            pointTMP.text = PlayerPrefs.GetInt(playerPrefsPoints, 0).ToString();

            FadeAchievementAnimation(newEarnAchievement);
        }
    }

    #region Fade via code
    //private IEnumerator HideAchievement(GameObject newEarnAchievement)
    //{
    //    yield return new WaitForSeconds(3);// after 3 second
    //    Destroy(newEarnAchievement);
    //}

    //private IEnumerator FadeAchievementCanvasGroup(GameObject achievement)
    //{
    //    CanvasGroup canvasGroup = achievement.GetComponent<CanvasGroup>();

    //    float rate = 1.0f / fadeAnimationSpeed;

    //    int startAlpha = 0;
    //    int endAlpha = 1;

    //    for (int i = 0; i < 2; i++)
    //    {
    //        float progress = 0.0f;

    //        while (progress < 1.0)
    //        {
    //            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, progress);

    //            progress += rate * Time.deltaTime;
    //            yield return null;
    //        }

    //        yield return new WaitForSeconds(2);
    //        startAlpha = 1;
    //        endAlpha = 0;
    //    }
    //    Destroy(achievement);
    //}
    #endregion  Fade via code

    // Fade via Animation
    public void FadeAchievementAnimation(GameObject achievement)
    {
        Animator animator = achievement.GetComponent<Animator>();
        animator.speed = 1 * fadeAnimationSpeed;
    }

    public void DistroyGameObject(GameObject achievement)
    {
        Destroy(achievement);
    }

    #endregion Earn Achievement

    public void ResetAchievement()
    {
        foreach (Transform achievement in generalList.transform)
        {
            Destroy(achievement.gameObject);
        }

        foreach (Transform achievement in otherList.transform)
        {
            Destroy(achievement.gameObject);
        }

        achievements.Clear();

        PlayerPrefs.DeleteAll();

        pointTMP.text = PlayerPrefs.GetInt(playerPrefsPoints, 0).ToString();

        GenerateAchievement();
    }

    public Transform GetCategoryParent(Category category)
    {
        if (category == Category.General)
        {
            return generalList.transform;
        }
        if (category == Category.Other)
        {
            return otherList.transform;
        }
        else
        {
            return generalList.transform;
        }
    }
}

// Just add
public enum Category { General, Other }