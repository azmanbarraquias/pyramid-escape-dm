/*
* Copyright (c) Creative Code Studio
*/
using UnityEngine;

[System.Serializable]
public class MCIMG_Question
{

	[Header("Question")]
	public string question = "What is This?";
	public Sprite images;
    public AudioClip sound;
	[Space]
	public bool button1;
	public Sprite button1IMG;

	[Space]
	public bool button2;
	public Sprite button2IMG;

	[Space]
	public bool button3;
	public Sprite button3IMG;

	public Sprite correctIMG;
}