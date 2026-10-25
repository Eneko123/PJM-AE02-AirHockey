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
    public event Action PuntutationChanged;
    public event Action<int> RoundReset;

    public const int PointsToWin = 7;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        Instance = this;

        State = GameState.Menu;

        Time.timeScale = 0;
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    public void PuntuationChange(int pointToP1, int pointToP2)
    {
        if (State != GameState.Playing) return;
        P1Points += pointToP1;
        P2Points += pointToP2;
        PuntutationChanged?.Invoke();

        if (P1Points >= PointsToWin || P2Points >= PointsToWin)
        {
            P1Win = P1Points > P2Points;
            FinishGame();
        }
        else
        {
            int receiver = pointToP1 > 0 ? 2 : 1;
            RoundReset?.Invoke(receiver);
        }
    }

    public void StartGame()
    {
        if (State != GameState.Menu && State != GameState.Finished) return;
        State = GameState.Playing;
        Time.timeScale = 1f;
        P1Points = 0;
        P2Points = 0;
        P1Win = false;
        StateChanged?.Invoke(State);
        PuntutationChanged?.Invoke();
        RoundReset?.Invoke(UnityEngine.Random.Range(1, 3));   // saque inicial aleatorio
    }

    public void FinishGame()
    {
        if (State != GameState.Playing) return;
        State = GameState.Finished;
        Time.timeScale = 0f;
        StateChanged?.Invoke(State);
    }
}
