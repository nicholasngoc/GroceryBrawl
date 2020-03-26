using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    /// <summary>
    /// Method to start the coroutine to destroy this gameobject
    /// after a timer
    /// </summary>
    /// <param name="destroyTime">Amount of time before this obj is destroyed</param>
    public void StartDestroyTimer(float destroyTime)
    {
        StartCoroutine(DestroyTimer(destroyTime));
    }


    /// <summary>
    /// Coroutine that destroys this gameobject after a specified
    /// amount of time
    /// </summary>
    /// <param name="destroyTime">Amount of time before this obj is destroyed</param>
    /// <returns></returns>
    private IEnumerator DestroyTimer(float destroyTime)
    {
        yield return new WaitForSeconds(destroyTime);

        Destroy(gameObject);
    }
}
