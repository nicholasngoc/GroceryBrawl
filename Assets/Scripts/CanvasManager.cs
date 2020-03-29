using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public GameManager gameManager;

    public Text timerText;
    public Text scoreText;
    public Text gameOverText;

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

        if(timerText != null)
            timerText.text = "Time: " + (int)gameManager.gameTime;
        if(scoreText.text != null)
            scoreText.text = "Score: " + gameManager.playerScore.Score;
    }
}
