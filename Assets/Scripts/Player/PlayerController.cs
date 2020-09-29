using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float jump_force = 7f;
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private LayerMask layer;

    private Rigidbody2D player;
    private BoxCollider2D collider;
    private void Start()
    {
        player = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
    }
    private void FixedUpdate()
    {
        player.velocity = new Vector2(speed * Input.GetAxis("Horizontal"), player.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && is_grounded())
            player.velocity = new Vector2(player.velocity.x, jump_force);
    }

    private bool is_grounded() {
        RaycastHit2D hit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, .5f, layer);
        return hit.collider != null;
    }
}
