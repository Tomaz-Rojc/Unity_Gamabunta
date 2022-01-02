using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySoundManager : MonoBehaviour
{
    public static AudioClip keySound, dashSound, jumpSound;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        keySound = Resources.Load<AudioClip>("keySound");
        dashSound = Resources.Load<AudioClip>("dashSound");
        jumpSound = Resources.Load<AudioClip>("jumpSound");

        audioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip) {
        switch (clip) {
            case "key":
                audioSrc.PlayOneShot(keySound);
                break;
            case "dash":
                audioSrc.PlayOneShot(dashSound);
                break;
            case "jump":
                audioSrc.PlayOneShot(jumpSound);
                break;
        }
    }
}
