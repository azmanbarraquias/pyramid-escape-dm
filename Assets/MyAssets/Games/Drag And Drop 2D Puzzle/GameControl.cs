using UnityEngine;
using TMPro;

public class GameControl : MonoBehaviour
{
	public GameObject winTxt;
	public GameObject starEffect;
	// public TextMeshProUGUI points;

    public AudioSource audioSource;
    public AudioClip aClip;

    // Update is called once per frame
    void Update()
    {
		// points.text = DragAndDrop.i.ToString() + " / 4";
		if (DragAndDrop.i == 4)
		{
			winTxt.SetActive(true);
			starEffect.SetActive(true);
			ExitDAD();
		}
    }

	public void ExitDAD()
	{
		DragAndDrop.i = 0;
	}

    public void PlayCorrectPlaceSound()
    {
        audioSource.clip = aClip;
        audioSource.Play();
    }
}
