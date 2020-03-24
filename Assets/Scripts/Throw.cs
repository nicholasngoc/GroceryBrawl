using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that handles the food throw action by the player
/// </summary>
public class Throw : MonoBehaviour
{
    private GameObject[] _foodPrefabs;
    public GameObject throwSpawnPoint;
    public float throwForce;
    public float randAngularVelModifier;

    [Header("Can Throw")]
    private bool _canThrow = true;
    public float throwDelayMax;
    private float _throwDelayCount;

    private void Awake()
    {
        /**
         * In order to get all of the food prefabs that can be thrown I put all the prefabs in a folder in the Resources folder
         * and load them all through script rather than manually dragging each food item into the inspector
         */

        Object[] loadedFood = Resources.LoadAll("FoodPrefabs", typeof(GameObject));

        _foodPrefabs = new GameObject[loadedFood.Length];
        for(int x = 0; x < loadedFood.Length; x++)
        {
            _foodPrefabs[x] = (GameObject)loadedFood[x];
        }
    }

    private void Update()
    {
        if(_canThrow && Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //Default force vector that just used the .forward
            Vector3 forceVector = throwSpawnPoint.transform.forward * throwForce;

            //Modifies the force vector if we hit something. I don't use raycasts often so I think this doesnt have a range?...
            if (Physics.Raycast(ray, out hit))
                forceVector = Vector3.Normalize(hit.point - throwSpawnPoint.transform.position) * throwForce;

            Rigidbody foodRb = Instantiate(_foodPrefabs[Random.Range(0, _foodPrefabs.Length)],
                throwSpawnPoint.transform.position, throwSpawnPoint.transform.rotation).GetComponent<Rigidbody>();

            //This adds a random angular velocity to give the food a lil spin
            foodRb.angularVelocity = new Vector3(Random.Range(-randAngularVelModifier, randAngularVelModifier),
                Random.Range(-randAngularVelModifier, randAngularVelModifier), Random.Range(-randAngularVelModifier, randAngularVelModifier));

            foodRb.AddForce(forceVector);

            _throwDelayCount = throwDelayMax;
            _canThrow = false;
        }
        else if(!_canThrow && _throwDelayCount > 0)
        {
            _throwDelayCount -= Time.deltaTime;
        }
        else if(!_canThrow && _throwDelayCount < 0)
        {
            _canThrow = true;
        }
    }
}
