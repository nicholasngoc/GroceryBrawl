using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a model class attached to both the player and
/// enemies that handles the score system
/// </summary>
public class ScoreModel : MonoBehaviour
{
    private int _score;
    public int Score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;

            //The following displays an amount of bags based on the score value
            if(_bags != null)
            {
                for(int x = 0; x < _bags.Length; x++)
                {
                    if (x < _score)
                        _bags[x].SetActive(true);
                    else
                        _bags[x].SetActive(false);
                }
            }
        }
    }
    public Transform bagParent; //Parent obj that has the bag obj's as it's children
    private GameObject[] _bags;

    private void Awake()
    {
        _bags = new GameObject[bagParent.childCount];

        for(int x = 0; x < _bags.Length; x++)
        {
            _bags[x] = bagParent.GetChild(x).gameObject;
        }

        //This forces the bags to adjust to the initial score
        Score = _score;
    }

    private void Start()
    {
        //StartCoroutine(TestScore());
    }

    private IEnumerator TestScore()
    {
        for(int x = 0; x < _bags.Length; x++)
        {
            Score++;
            print(Score);

            yield return new WaitForSeconds(1);
        }

        for(int x = _bags.Length - 1; x >= 0; x--)
        {
            Score--;
            print(Score);

            yield return new WaitForSeconds(1);
        }

        yield return null;
    }
}
