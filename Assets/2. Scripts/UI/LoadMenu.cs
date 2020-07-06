using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenu : MonoBehaviour
{

    public Animator transition;

    public float transitionTime;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            LoadnextlevelIndex();
        }
    }


    public void LoadnextlevelIndex()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));      
    }

    public void LoadnextlevelString(string levelString)
    {
        StartCoroutine(LoadLevelString(levelString));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime * Time.deltaTime);

        SceneManager.LoadScene(levelIndex);
    }

    IEnumerator LoadLevelString(string levelString)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime * Time.deltaTime);

        SceneManager.LoadScene(levelString);
    }
}
