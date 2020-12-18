using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MainGameManager : MonoBehaviour
{
    public static MainGameManager instance;
    public enum MainGameState
    {
        Main, Game
    }
    
    public MainGameState gameState
    {
        get => _mainGameState;
        set
        {
            _mainGameState = value;
            switch (value)
            {
                case MainGameState.Main:
                    mainListener.Invoke();
                    break;
                case MainGameState.Game:
                    gameListener.Invoke();
                    break;
            }
        }
    }

    public UnityEvent mainListener = new UnityEvent();
    public UnityEvent gameListener = new UnityEvent();

    public event Action GrowMainScene;
    public void OnGrowMainScene()
    {
        if(GrowMainScene != null) GrowMainScene.Invoke();
    }
    public event Action ShrinkMainScene;
    public void OnShrinkMainScene()
    {
        if(ShrinkMainScene != null) ShrinkMainScene.Invoke();
    }
    public event Action GameLose;
    public void OnGameLose()
    {
        if(GameLose != null) GameLose.Invoke();
    }


    private MainGameState _mainGameState;
    
    [SerializeField] private Text impactText;
    [SerializeField] private GameObject foodSilhouette;
    [HideInInspector] public int roundNumber;


    private const int StartingLives = 3;
    [HideInInspector] public int remainingLives;
    private int numberOfGames;
    private List<string> _remainingGames;

    private const float ShortTime = 3.4286f;
    private const float LongTime = 6.8571f;
    public const float halfBeat = .23333f;

    public float startWaitTime;
    public int indexOffset;
    [SerializeField] private int roundsToWin;

    private void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(gameObject);
        numberOfGames = SceneManager.sceneCountInBuildSettings - 3;
    }

    private void Start()
    {
        GameManager.Instance.mainGameListener.AddListener(StartGame);
    }

    public void StartGame()
    {
        remainingLives = StartingLives;
        roundNumber = 1;
        _remainingGames = new List<string>();
        for(int i = 0; i < numberOfGames; i++) _remainingGames.Add(NameFromIndex(i+indexOffset));
        StartCoroutine(LoadFirstGame());
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("TitleScreen");
        SceneManager.LoadScene("Main");
        StartGame();
    }

    private static string NameFromIndex(int BuildIndex)
    {
        string path = SceneUtility.GetScenePathByBuildIndex(BuildIndex);
        int slash = path.LastIndexOf('/');
        string name = path.Substring(slash + 1);
        int dot = name.LastIndexOf('.');
        print(name.Substring(0, dot));
        return name.Substring(0, dot);
    }
    
    struct GameInfo
    {
       public string name;
       public int id;
    }

    private IEnumerator LoadFirstGame()
    {
        yield return new WaitForSeconds(0);//TODO: Change to start wait time
        StartCoroutine(LoadNextGame());
    }
    
    private IEnumerator LoadNextGame()
    {
        yield return new WaitForSeconds(.1f);
        var nextGame = GetNextGame();
        var sceneName = nextGame.name;
        AsyncOperation scene = SceneManager.LoadSceneAsync(nextGame.id);
        scene.allowSceneActivation = false;
        SetImpactText(sceneName);
        yield return new WaitForSeconds(ShortTime - halfBeat - .31f);
        OnGrowMainScene();
        ImpactWord.instance.HandleImpactText();
        yield return new WaitForSeconds(.21f);
        scene.allowSceneActivation = true;
        LevelPreview.instance.HandleLevelPreview(true);
        
    }

    private void SetImpactText(string sceneName)
    {
        sceneName += "!";
        impactText.text = sceneName;
    }

    private GameInfo GetNextGame()
    {
        GameInfo game = new GameInfo();
        game.id = Random.Range(0, _remainingGames.Count);
        game.name = _remainingGames[game.id];
        game.id += indexOffset;
        //remainingGames.Remove(game);
        return game;
    }
    

    public void OnMinigameStart(Minigame minigame)
    {
        TimerManager.Instance.StartTimer(minigame.gameTime);
        var waitTime = minigame.gameTime == Minigame.GameTime.Short ? ShortTime : LongTime;
        StartCoroutine(WaitForMinigameEnd(minigame, waitTime));
    }

    private IEnumerator WaitForMinigameEnd(Minigame minigame, float time)
    {
        yield return new WaitForSeconds(.1f);
        AsyncOperation scene = SceneManager.LoadSceneAsync("Main");
        scene.allowSceneActivation = false;
        yield return new WaitForSeconds(time - .1f - halfBeat);
        
        LevelPreview.instance.HandleLevelPreview(false);
        yield return new WaitForSeconds(halfBeat);
        
        if (!minigame.gameWin) remainingLives -= 1;
        scene.allowSceneActivation = true;
        yield return null;
        OnShrinkMainScene();
        if (remainingLives == 0)
        {
            Invoke(nameof(OnGameLose), .1f);
        }
        else if (roundNumber < roundsToWin)
        {
            roundNumber++;
            StartCoroutine(LoadNextGame());
        }
        else
        {
            GameManager.Instance.LoadScene("End");
        }
    }
}
