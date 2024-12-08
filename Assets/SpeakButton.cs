using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakButton : MonoBehaviour
{

    private void Update() {
		if (FindObjectOfType<AudioManager>().GetSound("Speak").source.isPlaying)
		{
			this.gameObject.SetActive(true);
		}
		else
		{
			this.gameObject.SetActive(false);
		}
    }
    public void StopSpeak() {
        FindObjectOfType<AudioManager>().StopPlay("Speak");
    }
}
