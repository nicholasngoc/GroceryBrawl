using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public GameManager gameManager;

    private float totalTime;
    public Text timerText;
    public Text scoreText;
    public Image timer;
    public Color fullTime;
    public Color emptyTime;
    public Text gameOverText;

    private void Start()
    {
        totalTime =(int)gameManager.gameTime;
    }   
    private void Awake()
    {
        if(gameOverText != null)
            gameOverText.text = "";
    }

    private void Update()
    {
        /**
         * Note: as of right now these are only for testing.
         * We can change how the UI is later
         */
        timer.color = Color.Lerp(emptyTime,fullTime,gameManager.gameTime/totalTime);
        timer.fillAmount = (float)gameManager.gameTime/totalTime;
        if(timerText != null)
            timerText.text = "" + (int)System.Math.Ceiling(gameManager.gameTime);
        if(scoreText.text != null)
            scoreText.text = "Score: " + gameManager.playerScore.Score;
    }
}
