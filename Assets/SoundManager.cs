using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip ballHit;
    static AudioSource audioSrc;

    void Start()
    {
        ballHit = Resources.Load<AudioClip>("Pong");

        audioSrc = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }
    public static void Play(string clip)
    {
        switch (clip)
        {
            case "Hit":
                audioSrc.PlayOneShot(ballHit);
                break;

            default:
                break;
        }
    }
}
