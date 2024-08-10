using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollDiceManager : MonoBehaviour {

    public Text score;
    public Text highScore;
    public AudioSource aSource;
    private bool isRunning = false;
    public Animator diceAnimator;
    public Image diceImage;
    public Sprite[] diceImages; 

    void Start()
    {
        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString(); //PlayerPrefs.Get the Highcore value, if none default 0;
    }


    public void RollDice()
    {

        if (isRunning == true)
            return;
        
        StartCoroutine(GetRandomNumber());
    }

    IEnumerator GetRandomNumber()
    {
        isRunning = true;

        diceAnimator.gameObject.SetActive(true);

        aSource.Play();
        yield return new WaitForSeconds(3f);

        diceAnimator.gameObject.SetActive(false);


        int randomNumber = Random.Range(1, 7); // Random number between 1-6 but not the number 7
        diceImage.sprite = diceImages[randomNumber - 1];
        score.text = randomNumber.ToString(); //Convert randomNumber to string, hints ToString(STRING FORMAT)

        // if randomNumber greater than get value from PlayerPrefs.GetInt("HighScore", 0)); which is 0
        if (randomNumber > PlayerPrefs.GetInt("HighScore", 0))
        {
            // PlayerPrefs Stores and accesses player preferences between game sessions.
            // PlayerPrefs.SetVariableType("{1}," ,{2}); {1}key value or name, {2}value.
            // therefore we want to store or set value to "HighScore = randomNumber";
            PlayerPrefs.SetInt("HighScore", randomNumber);
            highScore.text = randomNumber.ToString();
        }
     
        isRunning = false;

    }

    public void Reset()
    {
        // PlayerPrefs.DeleteAll(); // This Delete all the record or key

        // This Delete only key value of HighScore on PlayerPrefs Memory
        PlayerPrefs.DeleteKey("HighScore"); 
        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        //  highScore.text = "0";

        diceImage.sprite = null;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Application.Quit();
        }
    }
}
