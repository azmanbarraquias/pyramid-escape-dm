
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestionItem
{

    [Header("Question")]
    public string question = "What is This?";


    [Space]
    public List<Answer> answers;



}

[System.Serializable]
public class Answer
{

    public bool isCorrectAnswer;
    public string answerText;


}







