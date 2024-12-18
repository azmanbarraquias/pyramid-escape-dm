using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GameControl : MonoBehaviour
{
	public GameObject winTxt;
	public GameObject starEffect;
	// public TextMeshProUGUI points;

    public AudioSource audioSource;
    public AudioClip aClip;

    public GameObject[] puzzles;

    private List<GameObject> puzzleDone = new List<GameObject>();


    public GameObject finishPuzzle;


    private static GameControl instance;
    public static GameControl Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameControl>();
            }
            return instance;
        }
    }

	
	 private void Start()
    {
		foreach (GameObject puzzle in puzzles)
        {
            puzzleDone.Add(puzzle);
        }
        randomPuzzle();
	}

	
    public void randomPuzzle()
    {
        if(puzzleDone.Count == 0)
        {
             FindObjectOfType<AudioManager>().Play("Collect");
            finishPuzzle.gameObject.SetActive(true);
              if (AchievementManager.Instance != null)
                    AchievementManager.Instance.EarnAchievement("Puzzle");
        return;

        }
        int index = Random.Range(0, puzzleDone.Count - 1);
        puzzleDone[index].SetActive(true);
        Debug.Log(index);
        puzzleDone.RemoveAt(index);
    }




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

        if(Input.GetKeyDown(KeyCode.Space)) {
        randomPuzzle();
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
