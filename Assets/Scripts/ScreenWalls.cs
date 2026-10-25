using UnityEngine;

public class ScreenWalls : MonoBehaviour
{
    [SerializeField] private float goalWidth = 1.75f;      // ancho del hueco (igual que tus porterías)
    [SerializeField] private float thickness = 1f;
    [SerializeField] private PhysicsMaterial2D wallMaterial;   // Friction 0, Bounciness ~0.9

    void Start()
    {
        Camera cam = Camera.main;
        float h = cam.orthographicSize;
        float w = h * cam.aspect;
        Vector2 c = cam.transform.position;

        // Laterales (altura extra para cubrir las esquinas)
        CreateWall("Left", c + new Vector2(-w - thickness / 2, 0), new Vector2(thickness, 2 * h + 2 * thickness));
        CreateWall("Right", c + new Vector2(w + thickness / 2, 0), new Vector2(thickness, 2 * h + 2 * thickness));

        // Arriba y abajo, cada uno en dos tramos dejando el hueco de la portería
        float segW = Mathf.Max(0.1f, w - goalWidth / 2);
        float segX = goalWidth / 2 + segW / 2;
        foreach (float ySign in new[] { 1f, -1f })
        {
            float y = ySign * (h + thickness / 2);
            CreateWall("Edge", c + new Vector2(-segX, y), new Vector2(segW, thickness));
            CreateWall("Edge", c + new Vector2(segX, y), new Vector2(segW, thickness));
        }
    }

    private void CreateWall(string wallName, Vector2 position, Vector2 size)
    {
        var go = new GameObject(wallName);
        go.transform.SetParent(transform);
        go.transform.position = position;
        var col = go.AddComponent<BoxCollider2D>();
        col.size = size;
        col.sharedMaterial = wallMaterial;
    }
}