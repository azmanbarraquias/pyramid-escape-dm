using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneLoader : MonoBehaviour
{
	#region My Variable's

	[Header("Scene Loader Setup")]
	public GameObject loadingScreen;

	public GameObject menuScreen;

	public Slider loadingBar;

	public TextMeshProUGUI progressText;

	private bool isLoadSceneStarted = false;

	[Header("Custom Load Scene Timer")]
	public bool useCustomLoading = false;

	public float timer = 10;

	private bool startCustomSceneLoader = false;
	private float startTime = 0;
	private int sceneIndex;

	#endregion  My Variable's

	private void Start()
	{
		Time.timeScale = 1f;
	}
	public void LoadSceneIndex(int sceneIndex)
	{
		menuScreen.SetActive(false); // Set Menu Panel Active = false
		loadingScreen.SetActive(true); // Set Loading Panel Active = true
		if (useCustomLoading == true && Time.timeScale != 0)
		{
			CustomLoadSceneLoader(sceneIndex);
		}
		else
		{
			// int for index, string for scene name
			if (isLoadSceneStarted == false)
			{ 
				StartCoroutine(LoadAsynchronously(sceneIndex)); 
			}
			else
			{ 
				Debug.Log("Load scene is already started. . ."); 
			}
		}
	}
	
	private void CustomLoadSceneLoader(int index)
	{
		sceneIndex = index;
		loadingBar.maxValue = timer;
		loadingScreen.SetActive(true);
		startCustomSceneLoader = true;
	}

	private void Update()
	{
		if (startCustomSceneLoader == true)
		{
			if (timer >= startTime)
			{
				startTime += Time.deltaTime;
				loadingBar.value = startTime;
				progressText.text = (Mathf.Clamp01(startTime / timer) * 100).ToString("0") + " %";
			}
			else
			{
				startCustomSceneLoader = false;
				Debug.Log("Load Scene started");
				SceneManager.LoadScene(sceneIndex);
			}
		}
	}

	private IEnumerator LoadAsynchronously(int _sceneIndex)
	{
		Debug.Log("Load Scene started, please wait. . .");

		isLoadSceneStarted = true;

		/* LoadSceneAsync load the scene a synchronously in the backgorund that
		 * means it keep all our current scene and all the behavior
		 * in it running while its loading our new scene in to memory */

		/* What it can do now is get some information back from our scene manager
		 * about the progress of this operation. */

		// not we store the info into the variable AsyncOperation name operation,
		AsyncOperation operation = SceneManager.LoadSceneAsync(_sceneIndex);

		// while not done
		while (!operation.isDone)
		{
			// insted 0 and 0.9 we use Mathf.Clamp01 to change the value to value 0 and 1
			float progress = Mathf.Clamp01(operation.progress / 0.9f);

			Debug.Log((progress * 100).ToString("0"));

			loadingBar.value = progress;

			progressText.text = (progress * 100).ToString("0") + "%";

			// wait next frame and then continuing
			yield return null;
		}
		isLoadSceneStarted = false;
	}

	public void QuitGame()
	{
		Application.Quit();
		Debug.Log("QuitGame");
		//UnityEditor.EditorApplication.isPlaying = false;
	}
}