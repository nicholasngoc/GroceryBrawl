using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple class that plays it's audio source and destroys the obj once it's finished playing
/// </summary>
public class SoundEffectModel : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(PlaySound());
    }

    private IEnumerator PlaySound()
    {
        YieldInstruction delay = new WaitForEndOfFrame();

        AudioSource source = GetComponent<AudioSource>();
        source.Play();

        while(source.isPlaying && source != null)
        {
            yield return delay;
        }

        Destroy(gameObject);

        yield return null;
    }
}
