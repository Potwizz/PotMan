using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource musicSound;
    public AudioClip musicPlaying;

    // Start is called before the first frame update
    void Start()
    {
        musicSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
