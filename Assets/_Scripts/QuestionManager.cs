using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    #region Variables
    [Header("Text Game Object")]
    // public TextMeshProUGUI questionNoInfo;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreTextAns;

    public GameObject unlockBtn;

    [Space]
    // public Image questionImage;
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI answerATMP;
    public TextMeshProUGUI answerBTMP;
    public TextMeshProUGUI answerCTMP;
    public TextMeshProUGUI answerDTMP;

    public GameObject answerC;
    public GameObject answerD;


    [Header("AnswerAnim")]
    // public Animator answer;

    [Header("FinishGameUI")]
    public GameObject endGamePanel;
    public GameObject mainGame;

    [Header("Number of Question")]
    public QuestionItem[] questions;

    // store the ununansweredQuestions.
    // Static so that when we reload the next scene it will remember the questions...
    private List<QuestionItem> unansweredQuestions;

    private List<QuestionItem> answeredQuestionistory = new List<QuestionItem>();

    private List<int> answeredHistory = new();

    private QuestionItem currentQuestion; //this will store question after get the random question

    //string correctQuestion; // store the correct question
    private int currentNoQestion;

    private int currentQuestionIndex;

    public GameObject asnweredHistoryTemplate;

    public GameObject asnwerTemplate;

    public Transform asnweredHistoryHolder;

    public int Level;

    public GameObject nextLevelBtn;

    public Color correctColor;

    public Color wrongColor;

    public string unlockTitle;

    int total = 0;
    string result;
    // string ans;
    // public Image correctImgAnswer;

    // public AudioSource aSource;
    // public AudioClip audioClip;
    // public AudioClip correctSound;
    // public AudioClip wrongSound;
    // public AudioClip finishSound;

    // public AudioClip wrongAudioClip;
    // public Animation anim;
    // [Space]
    // public string achievementName;

    #endregion

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        GetRandomQuestion(); // When start the game we want to load the questions
    }

    public void WrongAnswerCorrection()
    {
        // correctImgAnswer.sprite = currentQuestion.correctIMG;
    }

    public void LoadSceneIndexF(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    #region GetRandomQuestionMethod
    void GetRandomQuestion()
    {
        // but first we must load all the question to unansweredQuestions
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            // store to a list of unansweredQuestions <<< questions
            unansweredQuestions = questions.ToList<QuestionItem>();
        }

        // questionNoInfo.text = (++currentNoQestion) + " / " + questions.Length; // 1++ / totalQuestions

        // Get the random question index from list from unansweredQuestions
        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);

        // lets select the question base on index
        currentQuestion = unansweredQuestions[randomQuestionIndex];

        answeredQuestionistory.Add(currentQuestion);

        // remove question after set that current question from unansweredQuestion list
        unansweredQuestions.RemoveAt(randomQuestionIndex);

        // Set info to UI Text
        // if (currentQuestion.sound != null)
        // {
        //     aSource.Stop();
        //     aSource.clip = currentQuestion.sound;
        //     aSource.Play();
        // }

        // setQuestion
        questionText.text = currentQuestion.question;

        // questionImage.sprite = currentQuestion.images;
        if (currentQuestion.answers.Count == 4)
        {

            Debug.Log("4. " + currentQuestion.answers.Count);
            answerC.SetActive(true);
            answerD.SetActive(true);
            answerATMP.text = currentQuestion.answers[0].answerText;
            answerBTMP.text = currentQuestion.answers[1].answerText;
            answerCTMP.text = currentQuestion.answers[2].answerText;
            answerDTMP.text = currentQuestion.answers[3].answerText;

        }
        else
        {
            Debug.Log("2. " + currentQuestion.answers.Count);
            answerC.SetActive(false);
            answerD.SetActive(false);
            answerATMP.text = currentQuestion.answers[0].answerText;
            answerBTMP.text = currentQuestion.answers[1].answerText;
        }




    }
    #endregion

    #region Buttons
    public void SelectButton(int index)
    {
        answeredHistory.Add(index);
        if (currentQuestion.answers[index].isCorrectAnswer)
        {
            total++;

            //     aSource.Stop();
            //     aSource.clip = correctSound;
            //     aSource.Play();
            //     answer.SetTrigger("Correct");
            // Debug.Log("Correct");
        }
        else
        {
            // aSource.Stop();
            // aSource.clip = wrongSound;
            // aSource.Play();
            // answer.SetTrigger("Wrong");
            // if (correctImgAnswer != null)
            // {
            //     // correctImgAnswer.sprite = currentQuestion.correctIMG;
            // }
            // Debug.Log("Wrong");
        }
        GetNextQuestion();
    }

    #endregion

    #region GetNextAnswer
    public void GetNextQuestion()
    {
        // Check if unansweredQuestions is empty or all the question is answered, else GetRandomQuestion();
        if (unansweredQuestions.Count == 0 || unansweredQuestions == null)
        {
            // answer.gameObject.SetActive(false);

            // aSource.Stop();
            // aSource.clip = finishSound;
            // aSource.Play();

            if (total == questions.Length)
            {
                // AchievementManager.Instance.UnlockAchievement(achievementName);
                result = "PERFECT !!!";
                nextLevelBtn.SetActive(true);
               if(PlayerPrefs.GetInt(PlayerPrefsID.pyLevel, 1) <= Level)
                     PlayerPrefs.SetInt(PlayerPrefsID.pyLevel, ++Level);
                scoreTextAns.text = "Score  " + total.ToString() + " OUT OF " + questions.Length + "\n" + "PASSED";
                if (AchievementManager.Instance != null)
                    AchievementManager.Instance.EarnAchievement(unlockTitle);
            }
            else if (total > (questions.Length / 2))
            {
                result = "GOOD JOB";
                nextLevelBtn.SetActive(true);

                if(PlayerPrefs.GetInt(PlayerPrefsID.pyLevel, 1) <= Level)
                     PlayerPrefs.SetInt(PlayerPrefsID.pyLevel, ++Level);
                scoreTextAns.text = "Score: " + total.ToString() + " OUT OF " + questions.Length + "\n" + "PASSED";
                if (AchievementManager.Instance != null)
                    AchievementManager.Instance.EarnAchievement(unlockTitle);
            }
            else
            {
                FindObjectOfType<AudioManager>().Play("Try");
                result = "NICE TRY";
                scoreTextAns.text = "Score " + total.ToString() + " OUT OF " + questions.Length + "\n" + "FAILED";
                nextLevelBtn.SetActive(false);


            }
            scoreText.text = "YOUR SCORE IS " + total.ToString() + " OUT OF " + questions.Length + "  " + result;
            endGamePanel.SetActive(true);
            mainGame.SetActive(false);
            for (int i = 0; i < answeredQuestionistory.Count; i++)

            {
                var current = answeredQuestionistory[i];

                GameObject asnQuestion = Instantiate(asnweredHistoryTemplate, asnweredHistoryHolder);
                var asnwersH = current.answers[answeredHistory[i]];


                for (int j = 0; j < answeredQuestionistory[i].answers.Count; j++)
                {

                    GameObject asn = Instantiate(asnwerTemplate, asnQuestion.transform);
                    //  asn.transform.GetComponent<TextMeshProUGUI>().text = $"{answeredQuestionistory[i].answers[j]} = {current.answers[answeredHistory[i]].isCorrectAnswer == true}";

                    asn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{answeredQuestionistory[i].answers[j].answerText}";
//                    asn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{answeredQuestionistory[i].answers[j].answerText} = {answeredQuestionistory[i].answers[j].isCorrectAnswer}";

                    if (answeredHistory[i] == j)
                    {
                        asn.transform.GetChild(3).gameObject.SetActive(true);
                        if (current.answers[answeredHistory[i]].isCorrectAnswer)
                        {
                            asn.transform.GetChild(1).gameObject.SetActive(true);
                            asn.GetComponent<Image>().color = correctColor;
                        }
                        else
                        {
                            asn.transform.GetChild(2).gameObject.SetActive(true);
                            asn.GetComponent<Image>().color = wrongColor;
                        }
                    }


                }

                asnQuestion.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"  Question{i + 1}:";
                asnQuestion.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = answeredQuestionistory[i].question;



            }
        }
        else
        {
            GetRandomQuestion();
            // answer.SetTrigger("CloseAnswer");
        }
    }
    public void ExitGame()
    {
        unansweredQuestions.Clear();
    }

    public void PlaySampleQuestion()
    {
        // aSource.clip = audioClip;
        // aSource.Play();
        // anim.Play("MC_GameSampleAnim");
    }

    public void WrongSound()
    {
        // aSource.clip = wrongAudioClip;
        // aSource.Play();
    }
    public void StopSound()
    {
        // aSource.Stop();
    }

    #endregion
}