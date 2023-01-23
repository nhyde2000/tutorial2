using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip winSound;

    public static AudioClip loseSound;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        // Draws sound effect from resources folder. (NAME FOLDER "Resources"!!)
        winSound = Resources.Load<AudioClip>("WinFantasia");

        loseSound = Resources.Load<AudioClip>("lose sound 2 - 1_0");

        audioSrc = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "WinFantasia":
                audioSrc.PlayOneShot(winSound);
                break;
        }
        switch (clip)
        {
            case "lose sound 2 - 1_0":
                audioSrc.PlayOneShot(loseSound);
                break;
        }
    }
    
}