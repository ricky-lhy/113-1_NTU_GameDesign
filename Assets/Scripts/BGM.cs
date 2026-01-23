using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGM : MonoBehaviour
{
    AudioSource audioPlayer;
    [SerializeField] AudioClip backgroundMusic;
    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        audioPlayer.clip = backgroundMusic;
        audioPlayer.loop = true;
        audioPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
