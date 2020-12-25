using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        MainGameManager.Instance.GrowMainScene += GrowScene;
        MainGameManager.Instance.MainStart += ShrinkScene;
    }

    private void GrowScene()
    {
        _animator.Play("main-scene-grow");
    }

    private void ShrinkScene(bool win)
    {
        _animator.Play("main-scene-shrink");
    }

    private void OnDestroy()
    {
        MainGameManager.Instance.GrowMainScene -= GrowScene;
        MainGameManager.Instance.MainStart -= ShrinkScene;
    }
}
