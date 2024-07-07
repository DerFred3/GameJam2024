using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KuckuckSound : MonoBehaviour
{
    [SerializeField] private int delay;
    public AudioSource audioSource;
    public AudioClip kuckuckSound;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlaySounds());
    }

    private IEnumerator PlaySounds()
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            audioSource.PlayOneShot(kuckuckSound);
        }
    }
}
