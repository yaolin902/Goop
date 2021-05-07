using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : Singleton<BackgroundMusic> //Plays Background Music for levels 
{
    // Attach Script to Game Object (possibly camera?)
    // Attach Audio Source Component and select desired clip. Make sure to uncheck PLay on Awake
    AudioSource backgroundMusic;
    protected BackgroundMusic() { }
    public AudioClip clip;
    bool isLastSong = false;
    bool playing = false;
    int clipIndex = 0;
    private void Start()
    {
        if (!playing) {
            backgroundMusic = GetComponent<AudioSource>();
            backgroundMusic.clip = clip;
            play_bgm();
        }
    }
    // private void Update()
    // {
    //     if (!backgroundMusic.isPlaying) //if clip is not palying
    //     {
    //         clipIndex++; //play the next clip
    //         backgroundMusic.clip = clips[clipIndex];
    //         backgroundMusic.Play();
    //     }
    //     if (clipIndex == clips.Length - 1 && !isLastSong) //If we reached last clip 
    //     {
    //         backgroundMusic.loop = true; //loop it
    //         isLastSong = true; //set true so condition does not keep running 
    //     }
    // }
    public void play_bgm() {
        if (!backgroundMusic.isPlaying) {
            backgroundMusic.Play();
        }
    }

    public void stop_bgm() {
        backgroundMusic.Stop();
        playing = false;
    }
}
