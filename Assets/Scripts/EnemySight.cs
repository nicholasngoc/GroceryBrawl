using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySight : MonoBehaviour
{
    public float view = 180f;//ai view in degrees
    public int playerInSight = -1;//state var

    //might use the following for hearing
    //>>
    //public Vector3 playerLastPosition;
    //private NavMeshAgent agent;        

    private SphereCollider coll;//"length" of eyesight
    public GameObject[] player;//player reference

    private EnemyAI selfState;//reference to state machine

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
        coll = GetComponent<SphereCollider>();
        selfState = GetComponent<EnemyAI>();
        
    }

    void OnTriggerStay(Collider other)
    {
        bool behindWall = false;

        //check if the player is in sight
        if (GameObject.ReferenceEquals(other.gameObject, player[0]))
        {
            playerInSight = -1;//default false

            Vector3 directionPlayer = other.transform.position - transform.position; //make a vector pointing at the player
            float angle = Vector3.Angle(directionPlayer, transform.forward);//find angle between forward self and the player

            if (angle < 0.5f * view)//if player is less than half of our view angle... //0.5
            {
                RaycastHit wallChecker;
                if (Physics.Raycast(transform.position + Vector3.Scale(transform.up, new Vector3(0.5f,0.5f,0.5f)), directionPlayer.normalized, out wallChecker, coll.radius))//if there is a collider
                {
                    print(wallChecker.collider.gameObject);
                    if (GameObject.ReferenceEquals(wallChecker.collider.gameObject, player[0]) && selfState.isChasing == 1)//if it's the player and we're chasing them
                    {
                        playerInSight = 1;//we can see the player
                    }

                    else if (GameObject.ReferenceEquals(wallChecker.collider.gameObject, player[0]) && selfState.isShopping == 1)//if it's the player and we're shopping
                    {
                        print(wallChecker);
                        playerInSight = 1;//we can see the player
                    }
                    //else if (GameObject.ReferenceEquals(wallChecker.collider.gameObject, player[0]) && selfState.isRunningAway == 1)//if it's the player and we're searching
                    //{
                    //    playerInSight = -1;//we've already hit the player so we'll keep running
                    //}
                    else
                    {
                        behindWall = true;
                    }
                }
            }
        }
        if (selfState.isChasing == 1 && behindWall)//if chasing, as long as player is in the range keep chasing unless they escape viewing range
        {
            //print("player is in range although wall");
            playerInSight = 1;
        }

        //make-shift onTriggerExit
        if (Mathf.Abs(player[0].transform.position.x - transform.position.x) > coll.radius && Mathf.Abs(player[0].transform.position.z - transform.position.z) > coll.radius)
        {
            //print("you've outrun me");
            if (GameObject.ReferenceEquals(other.gameObject, player[0]))//if player is not in vision
            {
                //if (selfState.isChasing == 1)
                //{
                //    player is missing!
                //    playerMissing = 1;
                //    playerInSight = -1;
                //}
                //selfState.agent.destination = selfState.lastPlayerSight.position;//go to where the player last was before they disappeared
                playerInSight = -1;//can no longer see the player
            }
        }
    }
}
