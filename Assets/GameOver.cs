using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public static GameOver instance;
    [SerializeField] GameObject menu;
    [SerializeField] Text scoreTxt;
    [SerializeField] ScoreScript scoreScript;

    private void Awake()
    {
        instance = this;
    }

    public void ShowGameOver()
    {
        menu.SetActive(true);
        scoreTxt.text = PlayerPrefs.GetString("username") + ": " + scoreScript.scoreAmount;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
