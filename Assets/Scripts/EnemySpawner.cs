using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    private GameObject[] _peoplePrefabs;

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
        /**
         * In order to get all of the people prefabs that can be thrown I put all the prefabs in a folder in the Resources folder
         * and load them all through script rather than manually dragging each food item into the inspector
         */

        Object[] loadedPeople = Resources.LoadAll("People", typeof(GameObject));
        _peoplePrefabs = new GameObject[loadedPeople.Length];
        for (int x = 0; x < loadedPeople.Length; x++)
        {
            _peoplePrefabs[x] = (GameObject)loadedPeople[x];
        }

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

                Animator ani = Instantiate(_peoplePrefabs[Random.Range(0, _peoplePrefabs.Length)], enemy.transform).GetComponent<Animator>();
                enemy.animation = ani;

                enemyCount++;

                yield return spawnDelay;
            }
            else
                yield return normalDelay;
        }

        yield return null;
    }
}
