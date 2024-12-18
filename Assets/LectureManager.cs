using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LectureManager : MonoBehaviour
{
   	public void callSound()
	{
		FindObjectOfType<AudioManager>().Play("Pop");
	}


	public void SpeakPlay(AudioClip audioClip)
	{
		FindObjectOfType<AudioManager>().PlayReadSound("Speak", audioClip);
	}

	public void StopSpeak()
	{
		FindObjectOfType<AudioManager>().StopPlay("Speak");
	}
}
