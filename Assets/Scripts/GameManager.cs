using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public PlayerController player;
    public EnemyController enemy;
    public GameObject soldier;

    private void Start()
    {
        //view = GetComponent<PhotonView>();
        player = GameObject.FindGameObjectWithTag("Player"). GetComponent<PlayerController>();
        player.enabled = true;
    }
    /*public Text scoreText;
    public Text highscoreText;

    public int initialScore;
    public int scoreAmount;
    public int amount = 100;

    private void Start()
    {
        initialScore = 0;
        scoreAmount = initialScore;
        
    }
    private void Update()
    {


        scoreText.text = initialScore.ToString("SCORE :" + " " + scoreAmount);  



    }
    public void IncreaseScore()
    {

        
        scoreAmount += amount;




    }
    */


    public void GameOver()
    {
       
            player.enabled = false;
        
        
        
    }

}
