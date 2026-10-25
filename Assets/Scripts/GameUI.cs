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
    [SerializeField] private Image panelBackColor;

    private Color P1Color = Color.red;
    private Color P2Color = Color.blue;

    private GameManager gm;

    void Start()
    {
        if (gm == null) gm = GameManager.Instance;

        gm.StateChanged += OnStateChanged;
        gm.PuntutationChanged += OnPuntuationChanged;

        playButton.onClick.AddListener(gm.StartGame);

        OnStateChanged(gm.State);
    }

    private void OnDestroy()
    {
        gm.StateChanged -= OnStateChanged;
        gm.PuntutationChanged -= OnPuntuationChanged;
    }

    private void OnPuntuationChanged()
    {
        P1Counter.text = gm.P1Points.ToString();
        P2Counter.text = gm.P2Points.ToString();
    }

    private void OnStateChanged(GameState state)
    {
        Debug.Log($"He cambiado de estado. Estado actual: {state}");

        switch (state)
        {   
            case GameState.Menu:
                startPanel.SetActive(true);
                gameplayPanel.SetActive(false);
            break;
            case GameState.Playing:
                gameplayPanel.SetActive(true);
                startPanel.SetActive(false);
            break;
            case GameState.Finished:
                startPanel.SetActive(true);
                gameplayPanel.SetActive(false);
                if (gm.P1Win)
                {
                    P1Text.SetActive(true);
                    P2Text.SetActive(false);
                    panelBackColor.color = P1Color;
                } 
                else
                {
                    P1Text.SetActive(false);
                    P2Text.SetActive(true);
                    panelBackColor.color = P2Color;
                }
            break;
            default:
                startPanel.SetActive(true);
                gameplayPanel.SetActive(false);
            break;
        }
    }
}
