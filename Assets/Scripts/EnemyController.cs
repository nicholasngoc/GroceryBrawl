﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that controls the enemy's behaviour involving the enemy
/// stun system as well as score being transfered both to and from
/// the enemy and the player
/// </summary>
public class EnemyController : MonoBehaviour
{
    private ScoreModel ScoreModel
    {
        get
        {
            return GetComponent<ScoreModel>();
        }
    }
    public EnemySpawner spawner;

    [Header("Stun Variables")]
    public bool isStunned = false;
    public float stunDurationMax;
    private float _stunDurationCount;

    [Header("Destroy Variables")]
    private bool setDestroy;
    public float destroyTime;

    private void Start()
    {
        //I gave the enemy a random score here. Feel free to change as desired
        ScoreModel.Score = Random.Range(1, 10);
    }

    private void Update()
    {
        //Handles stun duration
        if (isStunned)
        {
            _stunDurationCount -= Time.deltaTime;

            if (_stunDurationCount < 0)
            {
                isStunned = false;

                print("!isStunned");
            }
        }

        //Handles destory timer
        if(setDestroy)
        {
            destroyTime -= Time.deltaTime;

            if (destroyTime < 0)
                Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        spawner.enemyCount--;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //This stuns the enemy if hit by a fruit
        if(!isStunned && collision.gameObject.CompareTag("Fruit"))
        {
            _stunDurationCount = stunDurationMax;
            isStunned = true;

            print("isStunned");
        }
        //This gives the player the enemies score if the enemy is stunned
        else if(isStunned && collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<ScoreModel>().Score += ScoreModel.Score;
            ScoreModel.Score = 0;

            setDestroy = true;
        }
        //This steals the players score if the enemy is not stunned and not set to be destroyed
        else if(!isStunned && !setDestroy && collision.gameObject.CompareTag("Player"))
        {
            ScoreModel playerScore = collision.gameObject.GetComponent<ScoreModel>();

            ScoreModel.Score += playerScore.Score;
            playerScore.Score = 0;
        }
    }
}
