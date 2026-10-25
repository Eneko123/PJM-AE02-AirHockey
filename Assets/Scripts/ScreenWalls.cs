using UnityEngine;

public class ScreenWalls : MonoBehaviour
{
    [SerializeField] private Vector2 fieldSize = new Vector2(6f, 10f);
    [SerializeField] private float goalWidth = 1.75f;
    [SerializeField] private float thickness = 1f;
    [SerializeField] private PhysicsMaterial2D wallMaterial;

    public Vector2 FieldSize => fieldSize;

    void Start()
    {
        float h = fieldSize.y / 2f;
        float w = fieldSize.x / 2f;
        Vector2 c = transform.position;

        // Laterales (altura extra para cubrir las esquinas)
        CreateWall("Left", c + new Vector2(-w - thickness / 2, 0), new Vector2(thickness, 2 * h + 2 * thickness));
        CreateWall("Right", c + new Vector2(w + thickness / 2, 0), new Vector2(thickness, 2 * h + 2 * thickness));

        // Arriba y abajo, cada uno en dos tramos dejando el hueco de la porteria
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