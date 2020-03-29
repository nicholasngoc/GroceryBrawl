using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DetectEnemy : MonoBehaviour
{
    public float view = 90f;//ai view in degrees
    public int enemyInSight = -1;//state var

    //might use the following for hearing
    //>>
    //public Vector3 playerLastPosition;
    //private NavMeshAgent agent;        

    private SphereCollider coll;//"length" of eyesight
    public GameObject[] enemy;//player reference

    private EnemyAI selfState;//reference to state machine

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        coll = GetComponent<SphereCollider>();
        selfState = GetComponent<EnemyAI>();
    }

    void OnTriggerStay(Collider other)
    {
        //bool hiding = player[0].GetComponent<playerStateTest>().isHiding;
        //bool behindWall = false;

        //check if the enemy is in sight
        if (GameObject.ReferenceEquals(other.gameObject, enemy[0]))
        {
            //--------------------------------------------------------------------------------------------------------------------------------------------
            //enemyInSight = -1;//default false
            //--------------------------------------------------------------------------------------------------------------------------------------------
            Vector3 directionEnemy = other.transform.position - transform.position; //make a vector pointing at the player
            float angle = Vector3.Angle(directionEnemy, transform.forward);//find angle between forward self and the player

            if (angle < 0.5f * view)//if player is less than half of our view angle...
            {
                RaycastHit wallChecker;
                if (Physics.Raycast(transform.position + transform.up, directionEnemy.normalized, out wallChecker, coll.radius))//if there is a collider
                {
                    if (GameObject.ReferenceEquals(wallChecker.collider.gameObject, enemy[0]) && selfState.isShopping == 1)//if we're shopping
                    {
                        enemyInSight = 1;//we see another enemy
                    }
                    else if (GameObject.ReferenceEquals(wallChecker.collider.gameObject, enemy[0]) && selfState.isRunningAway == 1)//if we're running
                    {
                        enemyInSight = 1;//we see another enemy
                    }
                    else
                    {
                        //behindWall = true;
                    }

                }
            }

        }

    }
}
