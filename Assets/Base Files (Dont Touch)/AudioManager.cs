﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip readyMusic;
    [SerializeField] private AudioClip successMusic;
    [SerializeField] private AudioClip failureMusic;
    [SerializeField] private AudioClip gameOverMusic;
    [SerializeField] private AudioClip startMusic;

    private float _volume = 1;
    private AudioSource _source;

    private void Awake()
    {
        _source = gameObject.AddComponent<AudioSource>();
        //_source.clip = music[Random.Range(0, music.Length-1)];
        MainGameManager.Instance.FirstMainStart += IntroMusic;
        MainGameManager.OnMainStart += StartMusic;
        MainGameManager.Instance.GameOver += LoseMusic;
        _source.Play();
    }

    private void IntroMusic()
    {
        StartCoroutine(IntroMusicSequence()); // Replace with intro music
    }

    private IEnumerator IntroMusicSequence()
    {
        _source.volume = _volume;
        _source.clip = startMusic;
        _source.Play();
        yield return new WaitForSeconds(MainGameManager.ShortTime * 1.5f + MainGameManager.halfBeat);
        _source.clip = readyMusic;
        _source.Play();
    }
    
    private void StartMusic(bool win)
    {
        StartCoroutine(MainMusicStart(win));
    }

    private IEnumerator MainMusicStart(bool win)
    {
        _source.clip = win ? successMusic : failureMusic;
        _source.volume = _volume;
        _source.Play();
        yield return new WaitForSeconds(MainGameManager.ShortTime / 2);
        if (MainGameManager.Instance.gameOver) yield break;
        _source.clip = readyMusic;
        _source.Play();
        yield return new WaitForSeconds(MainGameManager.ShortTime / 2);
        StartCoroutine(FadeMusic());
    }

    private void LoseMusic()
    {
        _source.clip = gameOverMusic;
        _source.volume = _volume;
        _source.Play();
    }

    private IEnumerator FadeMusic()
    {
        while (_source.volume > .1f)
        {
            _source.volume -= .05f;
            yield return new WaitForSeconds(.02f);
        }
        _source.Stop();
    }

    private void OnDestroy()
    {
        MainGameManager.Instance.FirstMainStart -= IntroMusic;
        MainGameManager.OnMainStart -= StartMusic;
        MainGameManager.Instance.GameOver -= LoseMusic;
    }
}
