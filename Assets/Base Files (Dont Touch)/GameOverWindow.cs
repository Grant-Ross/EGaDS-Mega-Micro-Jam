using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverWindow : MonoBehaviour
{
    [SerializeField] private GameObject window;

    private void Awake()
    {
        window.SetActive(false);
        MainGameManager.instance.GameLose += GameLose;
    }

    private void GameLose()
    {
        window.SetActive(true);
    }

    public void RestartButton()
    {
        window.SetActive(false);
        //FindObjectOfType<AudioManager>().StartMusic();
        MainGameManager.instance.RestartGame();
    }

    public void TitleButton()
    {
        window.SetActive(false);
        GameManager.instance.LoadScene("TitleScreen");
    }
    private void OnDestroy()
    {
        MainGameManager.instance.GameLose -= GameLose;
    }
}
