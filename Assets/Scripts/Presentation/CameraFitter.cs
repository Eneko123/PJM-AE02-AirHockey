using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFitter : MonoBehaviour
{
    [SerializeField] private ScreenWalls field;
    [SerializeField] private float margin = 0.5f;   // aire alrededor del campo

    private Camera cam;
    private int lastW, lastH;

    void Awake()
    {
        cam = GetComponent<Camera>();
        Fit();
    }

    void Update()
    {
        // Por si rota la pantalla o cambia la ventana del Simulator
        if (Screen.width != lastW || Screen.height != lastH) Fit();
    }

    private void Fit()
    {
        lastW = Screen.width;
        lastH = Screen.height;

        float aspect = (float)Screen.width / Screen.height;
        float halfH = field.FieldSize.y / 2f + margin;
        float halfW = field.FieldSize.x / 2f + margin;

        // Toma lo que haga falta para que quepa tanto el alto como el ancho
        cam.orthographicSize = Mathf.Max(halfH, halfW / aspect);
    }
}