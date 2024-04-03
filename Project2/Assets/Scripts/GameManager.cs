using System;

using UnityEngine;

public class GameManager : Singleton<GameManager>
{
 
    public static int mazeSize = 6;


    public static event Action<GameState> OnGameStateChanged;

    [SerializeField] public GameObject Canvas;


    public void UpdateGameState(GameState state){
        if(state != GameState.Restart){
            Canvas.SetActive(false);
        }
        OnGameStateChanged?.Invoke(state);
    }

    public void RestartGame(){
        UpdateGameState(GameState.Restart);
    }
    
   
    void Start()
    {   
        UpdateGameState(GameState.Initialization);
    }

 
}


public enum GameState{
    Initialization,
    NotStarted,
    Playing,
    GameOver,
    Restart
}