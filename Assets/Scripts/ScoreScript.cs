using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreScript : MonoBehaviour
{
    public static ScoreScript instance;

    public Text scoreText;
    public Text highscoreText;

    public int scoreAmount;
    public int amount = 100;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        scoreText.text = "SCORE : " + scoreAmount;
    }

    public void IncreaseScore()
    {
        scoreAmount += amount;
    }
}
