using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public class MinigameManager : MonoBehaviour
{
    //******* FOR DEBUGGING *******//
    public bool debugGameOnly; // set to true if you want to test your scene alone in play mode;
    //****************************//

    public Minigame minigame;
    
    
    

    
    public void PlaySound(string soundName)
    {
        foreach (var s in minigame.sounds)
        {
            if (s.soundName == soundName)
            {
                s.source.pitch = Random.Range(s.minPitch, s.maxPitch);
                s.source.Play();
            }
        }
    }
    private AudioSource _musicSource;
    private void Awake()
    {
        minigame.gameWin = false;
        if (!debugGameOnly && GameManager.Instance == null)
        {
            debugGameOnly = true;
            SceneManager.LoadScene("Main");
        }
        else
        {
            _musicSource = gameObject.AddComponent<AudioSource>();
            _musicSource.clip = minigame.music;
            foreach (var s in minigame.sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
            }
            StartCoroutine(GameDelayedStart());
        }
    }

    private IEnumerator GameDelayedStart()
    {
        yield return new WaitForSeconds(.2333f);
        MainGameManager.instance.OnMinigameStart(minigame);
        _musicSource.Play();
    }

}

