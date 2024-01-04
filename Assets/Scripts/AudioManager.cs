using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource myAudioSource;

    public AudioClip villageClip, woodsClip;

    void Awake()
    {
        myAudioSource = GetComponent<AudioSource>();
    }
    
    void Start()
    {
        
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.layer == 11)
        {
            myAudioSource.Stop();

            myAudioSource.PlayOneShot(woodsClip);

            myAudioSource.loop = true;
        }
    }
}
