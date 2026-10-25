using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Mallet : MonoBehaviour
{
    [SerializeField] private int playerIndex = 1;          // 1 = abajo, 2 = arriba
    [SerializeField] private TouchInputProvider input;
    [SerializeField] private Collider2D moveArea;          // zona de su mitad
    [SerializeField] private float maxSpeed = 30f;

    private Rigidbody2D rig;
    private float radius;
    private Camera cam;
    private GameManager gm;

    void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        radius = GetComponent<Collider2D>().bounds.extents.x;
        cam = Camera.main;
    }

    void Start() => gm = GameManager.Instance;

    void FixedUpdate()
    {
        if (gm.State != GameState.Playing) return;

        Vector2? screen = playerIndex == 1 ? input.P1Pos : input.P2Pos;
        if (screen == null) return;

        // Píxeles de pantalla = unidades del mundo
        Vector2 target = cam.ScreenToWorldPoint(screen.Value);

        // No salir de su mitad
        Bounds b = moveArea.bounds;
        target.x = Mathf.Clamp(target.x, b.min.x + radius, b.max.x - radius);
        target.y = Mathf.Clamp(target.y, b.min.y + radius, b.max.y - radius);

        // Velocidad limitada para no atravesar el disco
        rig.MovePosition(Vector2.MoveTowards(rig.position, target, maxSpeed * Time.fixedDeltaTime));
    }
}