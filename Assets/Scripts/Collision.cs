using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour // Script que j'ai fais moi même pour gérer si je suis sur un sol ou pas (pour le jump)
{

    [Header("Layers")]
    public LayerMask groundLayer;

    [Space]

    public bool onGround;

    [Space]

    [Header("Collision")]

    public float collisionRadius = 0.25f;
    public Vector2 collisionRadiusGround = new Vector2(0f, -1f);
    public Vector2 bottomOffset;
    private Color debugCollisionColor = Color.red;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        onGround = Physics2D.OverlapBox((Vector2)transform.position + bottomOffset, collisionRadiusGround, 0f, groundLayer);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube((Vector2)transform.position + bottomOffset, collisionRadiusGround);
    }
}