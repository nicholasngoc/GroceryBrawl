using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    //init vars
    public NavMeshAgent agent;
    private EnemySight selfSight;
    private DetectEnemy shopperHas;

    //states
    public int isShopping = -1;
    public int isChasing = -1;
    public int isRunningAway = -1;
    public int playerHit = -1;

    //roaming route
    public Transform[] pathPoints = new Transform[1];
    public int pathIndex = 0;
    private Transform playerPos;
    private GameObject[] player;
    public Transform lastPlayerSight;

    //constants
    public float shopSpeed = 4f;
    public float chaseSpeed = 6f;
    public float runSpeed = 8f;

    //init vars
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        selfSight = GetComponent<EnemySight>();
        //shopperHas = GetComponent<DetectEnemy>();
        player = GameObject.FindGameObjectsWithTag("Player");//player reference
        playerPos = player[0].transform;
        lastPlayerSight = playerPos;

        pathPoints[0] = GameObject.Find("WayPoint1").transform;
        pathPoints[1] = GameObject.Find("WayPoint2").transform; 
        pathPoints[2] = GameObject.Find("WayPoint3").transform;
        pathPoints[3] = GameObject.Find("WayPoint4").transform;
        pathPoints[4] = GameObject.Find("WayPoint5").transform;
        pathPoints[5] = GameObject.Find("WayPoint6").transform;
        pathPoints[6] = GameObject.Find("WayPoint7").transform;
        pathPoints[7] = GameObject.Find("WayPoint8").transform;
        pathPoints[8] = GameObject.Find("WayPoint9").transform;

        //init path
        int randomPoint = Mathf.FloorToInt(Random.Range(0, pathPoints.Length - 1));//pick a random waypoint
        pathIndex = randomPoint;
        agent.destination = pathPoints[randomPoint].position;//go to that waypoint
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Rigidbody>().velocity.x > 25 || GetComponent<Rigidbody>().velocity.z > 25)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
        }
        //if (playerHit == 1 && isChasing == 1)//if we hit them intentionally
        //{
        //    if (agent.remainingDistance <= agent.stoppingDistance)//stop running away once we've arrived at our shopping route
        //        isRunningAway = -1;
        //    else//otherwise    
        //        RunningAway();//run away
        //}
        if (selfSight.playerInSight == 1)//if the player is seen and we're not currently running away
        {
            Chasing();//chase them
        }
        else//otherwise
        {
            Shopping();//keep shopping
        }
    }

    void Chasing()
    {
        //update vars
        isShopping = -1;
        isChasing = 1;
        isRunningAway = -1;
        agent.speed = chaseSpeed;

        agent.destination = playerPos.position;//chase the player
        if (agent.remainingDistance <= agent.stoppingDistance)//if we have hit the player
        {
            playerHit = 1;
        }
    }

    void RunningAway()
    {
        //update vars
        isShopping = -1;
        isChasing = -1;
        isRunningAway = 1;
        agent.speed = runSpeed;

        agent.destination = pathPoints[pathIndex].position;//run back to your shopping route
    }

    void Shopping()
    {
        //update vars
        isShopping = 1;
        isChasing = -1;
        isRunningAway = -1;
        playerHit = -1;
        agent.speed = shopSpeed;

        if (agent.remainingDistance <= agent.stoppingDistance+3)//if we have arrived at a checkpoint or we are going to run into another shopper //|| shopperHas.enemyInSight == 1
        {
            int randomPoint = Mathf.FloorToInt(Random.Range(0, pathPoints.Length - 1));//pick a random waypoint
            pathIndex = randomPoint;
            agent.destination = pathPoints[randomPoint].position;//go to that waypoint
            print(pathPoints[randomPoint].position);
        }
    }

    void ogShopping()
    {
        //update vars
        isShopping = 1;
        isChasing = -1;
        isRunningAway = -1;
        playerHit = -1;
        agent.speed = shopSpeed;

        if (agent.remainingDistance <= agent.stoppingDistance)//if we have arrived at a checkpoint
        {
            if (pathIndex < pathPoints.Length - 1)//move on to the next pathPoints.Length
            {
                pathIndex++;//otherwise increment
            }
            else
            {
                pathIndex = 0;//reset to zero
            }
        }
        //print(pathIndex);
        agent.destination = pathPoints[pathIndex].position;//continue "shopping"
    }
}
