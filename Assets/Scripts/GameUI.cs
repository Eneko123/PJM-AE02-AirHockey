using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject gameplayPanel;

    [SerializeField] private Button playButton;
    [SerializeField] private GameObject P1Text;
    [SerializeField] private GameObject P2Text;

    [SerializeField] private TMP_Text P1Counter;
    [SerializeField] private TMP_Text P2Counter;

    private GameManager gm;

    void Start()
    {
        if (gm == null) gm = GameManager.Instance;

        playButton.onClick.AddListener(gm.StartGame);
    }

    private void OnStateChanged(GameState state)
    {
        startPanel.SetActive(state == GameState.Menu);
        gameplayPanel.SetActive(state == GameState.Playing);
        startPanel.SetActive(state == GameState.Finished);

        switch (state)
        {   
            case GameState.Finished:
                if(gm.P1Win)
                {
                    P1Text.SetActive(true);
                    P2Text.SetActive(false);
                } 
                else
                {
                    P1Text.SetActive(false);
                    P2Text.SetActive(true);
                }
            break;
        }
    }
}
