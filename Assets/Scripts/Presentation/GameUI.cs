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

    [SerializeField] private GameObject P1;
    [SerializeField] private GameObject P2;
    [SerializeField] private Transform P1Spawn;
    [SerializeField] private Transform P2Spawn;

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
        gm.PointsChanged += OnPuntuationChanged;

        playButton.onClick.AddListener(gm.StartGame);

        OnStateChanged(gm.State);
    }

    private void OnDestroy()
    {
        if (gm != null) gm.StateChanged -= OnStateChanged;
        if (gm != null) gm.PointsChanged -= OnPuntuationChanged;
    }

    private void OnPuntuationChanged()
    {
        P1Counter.text = gm.P1Points.ToString();
        P2Counter.text = gm.P2Points.ToString();
    }

    private void OnStateChanged(GameState state)
    {
        switch (state)
        {   
            case GameState.Menu:
                startPanel.SetActive(true);
                gameplayPanel.SetActive(false);
            break;
            case GameState.Playing:
                gameplayPanel.SetActive(true);
                startPanel.SetActive(false);

                P1.transform.position = P1Spawn.position;
                P2.transform.position = P2Spawn.position;
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
