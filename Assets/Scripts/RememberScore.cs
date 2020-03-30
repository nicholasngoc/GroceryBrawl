using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RememberScore : MonoBehaviour
{
    public static int score;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
