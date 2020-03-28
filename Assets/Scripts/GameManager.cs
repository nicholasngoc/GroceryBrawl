using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CanvasManager canvasManager;

    public GameState state;
    public float gameTime;
    public ScoreModel playerScore;

    private void Update()
    {
        if(gameTime < 0 && state != GameState.End)
        {
            state = GameState.End;

            if(canvasManager.gameOverText != null)
                canvasManager.gameOverText.text = "Game Over";
        }
        else if(gameTime > 0)
        {
            gameTime -= Time.deltaTime;
        }
    }
}

public enum GameState
{
    Default,
    End,
}
