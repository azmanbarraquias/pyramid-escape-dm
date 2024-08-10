using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public Animator animator;

    public int leveltoLoad;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            FadeToLevel(1);
        }
    }

    public void FadeToLevel(int levelIndex)
    {
        animator.speed = levelIndex;
        animator.SetTrigger("Start");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(leveltoLoad);
    }
}
