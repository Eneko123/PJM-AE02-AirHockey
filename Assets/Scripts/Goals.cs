using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Goals : MonoBehaviour
{
    [SerializeField] private int pointToP1;
    [SerializeField] private int pointToP2;
    [SerializeField] private Collider2D col;

    private GameManager gm;

    private void Start()
    {
        if (col == null) col = GetComponent<Collider2D>();
        if (gm == null) gm = GameManager.Instance;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Disc disc))
        {
            gm.PuntuationChange(pointToP1, pointToP2);
        }
    }
}
