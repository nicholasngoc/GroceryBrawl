using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameState state;
    public float gameTime;
    public ScoreModel playerScore;

    private void Update()
    {
        if(gameTime < 0 && state != GameState.End)
        {
            state = GameState.End;
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
