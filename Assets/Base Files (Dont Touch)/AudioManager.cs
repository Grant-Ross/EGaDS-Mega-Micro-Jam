using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] music;
    private AudioSource _source;

    private void Start()
    {
        _source = gameObject.AddComponent<AudioSource>();
        _source.clip = music[Random.Range(0, music.Length-1)];
        MainGameManager.instance.GameLose += LoseMusic;
        _source.Play();
    }

    public void StartMusic()
    {
        _source.Play();
    }

    private void LoseMusic()
    {
        _source.Stop();
    }

    private void OnDestroy()
    {
        MainGameManager.instance.GameLose -= LoseMusic;
    }
}
