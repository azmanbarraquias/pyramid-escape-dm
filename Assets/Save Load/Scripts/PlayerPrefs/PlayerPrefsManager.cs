using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerPrefsManager : MonoBehaviour
{
    // For more detail of PlayerPrefs visit the link:
    // https://docs.unity3d.com/ScriptReference/PlayerPrefs.html

    #region Save Setup;
    [Header("Save")]
    public TMP_InputField stringFieldSaveTMP;
    public TextMeshProUGUI MouseXSave_TMP, MouseYSave_TMP;
    public TextMeshProUGUI randomIntSaveTMP;
    public TextMeshProUGUI boolSaveTMP;
    public Toggle boolSaveToggle;

    #endregion Save Setup;

    private float mouseX;
    private float mouseY;
    private int randomNumber;

    #region Load Setup;
    [Header("Load")]
    public TMP_InputField stringFieldLoadTMP;
    public TextMeshProUGUI MouseXLoad_TMP, MouseYLoad_TMP;
    public TextMeshProUGUI randomIntLoadTMP;
    public TextMeshProUGUI boolLoadTMP;
    public Toggle boolLoadToggle;

    #endregion Load Setup;

    [Space]
    public bool resetOnPlay = false;

    private void Start()
    {
        if (resetOnPlay)
            ResetAll();
    }

    private void Update()
    {
        // track mouse position
        Vector3 mouse = Input.mousePosition;
        mouseX = mouse.x;
        mouseY = mouse.y;

        MouseXSave_TMP.text = "Mouse X: " + mouseX;
        MouseYSave_TMP.text = "Mouse Y: " + mouseY;
    }

    #region Save & Load string value
    public void Save_StringTextField()
    {
        if (!string.IsNullOrEmpty(stringFieldSaveTMP.text))
            PlayerPrefs.SetString("STRING", stringFieldSaveTMP.text);
    }

    public void Load_StringTextField()
    {
        stringFieldLoadTMP.text = PlayerPrefs.GetString("STRING", "No data found");
    }

    #endregion Save & Load string value

    #region Save & Load float value
    public void Save_FloatMousePosition()
    {
        PlayerPrefs.SetFloat("FLOAT_X", mouseX);
        PlayerPrefs.SetFloat("FLOAT_Y", mouseY);
    }

    public void Load_FloatMousePosition()
    {
        MouseXLoad_TMP.text = "Mouse X: " + PlayerPrefs.GetFloat("FLOAT_X", 0);
        MouseYLoad_TMP.text = "Mouse Y: " + PlayerPrefs.GetFloat("FLOAT_Y", 0);
    }

    #endregion Save & Load float value

    #region Save & Load int value + get random number
    public void GetRandomNumber()
    {
        int randNumber = Random.Range(1, 1000);

        randomNumber = randNumber;

        randomIntSaveTMP.text = randomNumber.ToString();
    }

    public void Save_IntRandomNumber()
    {
        PlayerPrefs.SetInt("INT", randomNumber);
    }

    public void Load_IntRandomNumber()
    {
        randomIntLoadTMP.text = PlayerPrefs.GetInt("INT", 0).ToString();
    }

    #endregion Save & Load int value + get random number

    #region Save & Load boolean value
    public void OnToogleChange()
    {
        boolSaveTMP.text = boolSaveToggle.isOn.ToString();
    }

    public void Save_BoolToggle()
    {
        // To Save bool, we use 1 == true, 0 == false;
        int saveValue = boolSaveToggle.isOn == true ? 1 : 0;
        PlayerPrefs.SetInt("BOOL", saveValue);
    }

    public void Load_BoolToggle()
    {
        bool loadValue = PlayerPrefs.GetInt("BOOL", 0) == 1 ? true : false;
        boolLoadToggle.isOn = loadValue;
        boolLoadTMP.text = loadValue.ToString();
    }

    #endregion Save & Load boolean value

    public void ResetAll()
    {
        PlayerPrefs.DeleteAll();

        // or specified specific key use PlayerPrefs.DeleteKey("NAME")
    }
}
