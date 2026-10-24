using System;
using UnityEngine;
using static UnityEngine.CullingGroup;

public enum GameState { Menu, Playing, Finished}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameState State { get; private set; }
    public int P1Points { get; private set; }
    public int P2Points { get; private set; }
    public bool P1Win { get; private set; }

    public event Action<GameState> StateChanged;

    private void Awake()
    {
        Instance = this;

        State = GameState.Menu;

        Time.timeScale = 0;
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    public void StartGame()
    {
        if (State != GameState.Menu) return;
        State = GameState.Playing;
        Time.timeScale = 1f;
        StateChanged?.Invoke(State);
    }

    public void FinishGame()
    {
        if (State != GameState.Playing) return;
        State = GameState.Finished;
        Time.timeScale = 0f;
        StateChanged?.Invoke(State);
    }
}
