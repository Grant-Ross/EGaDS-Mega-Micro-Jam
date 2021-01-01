using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundText : MonoBehaviour
{
    [SerializeField] private Text roundText;

    private void Start()
    {
        roundText.text = MainGameManager.Instance.roundNumber.ToString();
        MainGameManager.Instance.GameOver += GameLose;
    }

    private void GameLose()
    {
        gameObject.SetActive(false);
    }
    
    private void OnDestroy()
    {
        MainGameManager.Instance.GameOver -= GameLose;
    }
}
