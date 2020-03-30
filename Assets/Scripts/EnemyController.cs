using System.Collections;
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
    public Animator animation;
    public Rigidbody rb;

    [Header("Stun Variables")]
    public bool isStunned = false;
    public float stunDurationMax;
    private float _stunDurationCount;
    public ParticleSystem stunParticles;

    [Header("Destroy Variables")]
    private bool setDestroy;
    public float destroyTime;

    [Header("Audio")]
    public GameObject ramSFX;
    public GameObject fruitHitSFX;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        stunParticles.Stop();
    }

    private void Start()
    {
        //I gave the enemy a random score here. Feel free to change as desired
        ScoreModel.Score = Random.Range(1, 10);
    }

    private void Update()
    {
        /**
         * Notes: So in the animation controllers or whatever the var "Speed_f" is what determines
         * whether the animation is standing, walking, and running. For now I set it so that it corresponds
         * to the rigidbody's velocity but we can always change this. From what I can tell:
         * 
         * 0 = standing
         * 0.5 = walking
         * 1 = running
         */
        if(animation != null)
            animation.SetFloat("Speed_f", rb.velocity.magnitude);

        //Handles stun duration
        if (isStunned)
        {
            _stunDurationCount -= Time.deltaTime;

            if (_stunDurationCount < 0)
            {
                isStunned = false;

                stunParticles.Stop();

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
        if(collision.gameObject.CompareTag("Fruit"))
        {
            //Spawn sfx obj
            Instantiate(fruitHitSFX, transform);

            //This stuns the enemy if hit by a fruit
            if (!isStunned)
            {
                _stunDurationCount = stunDurationMax;
                isStunned = true;

                stunParticles.Play();
                print("isStunned");
            }
        }

        if(collision.gameObject.CompareTag("Player"))
        {
            //Spawn sfx obj
            Instantiate(ramSFX, transform);

            //This gives the player the enemies score if the enemy is stunned
            if (isStunned)
            {
                collision.gameObject.GetComponent<ScoreModel>().Score += ScoreModel.Score;
                ScoreModel.Score = 0;

                setDestroy = true;
            }
            //This steals the players score if the enemy is not stunned and not set to be destroyed
            else if(!isStunned && !setDestroy)
            {
                ScoreModel playerScore = collision.gameObject.GetComponent<ScoreModel>();

                ScoreModel.Score += playerScore.Score;
                playerScore.Score = 0;
            }
        }
    }
}
