using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    [Header("Location Variables")]
    public Transform spawnLocationParent;   //A parent obj with spawn locations as it's children
    private Transform[] _spawnLocations;

    [Header("Spawn Variables")]
    public float spawnRate;
    public int maxEnemies;
    public int enemyCount;
    public bool isSpawning;

    private void Awake()
    {
        //Gets reference to all spawn locations
        _spawnLocations = new Transform[spawnLocationParent.childCount];
        for(int x = 0; x < _spawnLocations.Length; x++)
        {
            _spawnLocations[x] = spawnLocationParent.GetChild(x);
        }

        isSpawning = true;
        StartCoroutine(SpawnRoutine());
    }

    /// <summary>
    /// Coroutine that keeps spawning enemies
    /// 
    /// To stop this coroutine just set isSpawning to false
    /// </summary>
    private IEnumerator SpawnRoutine()
    {
        YieldInstruction spawnDelay = new WaitForSeconds(spawnRate);
        YieldInstruction normalDelay = new WaitForEndOfFrame();

        while(isSpawning)
        {
            //Creates a new enemy if it can
            if (enemyCount < maxEnemies)
            {
                Transform randLocation = _spawnLocations[Random.Range(0, _spawnLocations.Length)];
                EnemyController enemy = Instantiate(enemyPrefab, randLocation.position, randLocation.rotation).GetComponent<EnemyController>();
                enemy.spawner = this;

                enemyCount++;

                yield return spawnDelay;
            }
            else
                yield return normalDelay;
        }

        yield return null;
    }
}
