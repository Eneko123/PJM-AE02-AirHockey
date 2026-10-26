using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Disc : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 15f;
    [SerializeField] private Transform spawnP1;
    [SerializeField] private Transform spawnP2;

    private Rigidbody2D rig;
    private GameManager gm;

    void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        gm = GameManager.Instance;
        gm.RoundReset += ResetDisc;
    }

    void OnDestroy()
    {
        if (gm != null) gm.RoundReset -= ResetDisc;
    }

    void FixedUpdate()
    {
        rig.linearVelocity = Vector2.ClampMagnitude(rig.linearVelocity, maxSpeed);
    }

    private void ResetDisc(int receivingPlayer)
    {
        rig.linearVelocity = Vector2.zero;
        rig.angularVelocity = 0f;
        rig.position = receivingPlayer == 1 ? spawnP1.position : spawnP2.position;
    }
}