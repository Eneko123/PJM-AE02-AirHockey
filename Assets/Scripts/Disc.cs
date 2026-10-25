using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Disc : MonoBehaviour
{
    private Rigidbody2D rig;
    private Collider2D col;
    
    void Start()
    {
        if (rig == null) rig = GetComponent<Rigidbody2D>();
        if (col == null) col = GetComponent<Collider2D>();
    }

}
