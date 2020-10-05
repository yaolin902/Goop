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
    private LayerMask ground_layer = LayerMask.NameToLayer("Ground");
    [SerializeField]
    private LayerMask platform_layer = LayerMask.NameToLayer("OneWayPlatform");

    private Rigidbody2D player;
    private BoxCollider2D collider;

    private bool is_facing_right = true;
    private void Start()
    {
        player = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
    }
    private void FixedUpdate()
    {
        // player movement
        float move_x = Input.GetAxis("Horizontal");
        player.velocity = new Vector2(speed * move_x, player.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && (is_grounded() || is_platformed()))
            player.velocity = new Vector2(player.velocity.x, jump_force);

        if (Input.GetKeyDown(KeyCode.DownArrow) && is_platformed())
            StartCoroutine(fall_down());

        // player direction
        if ((move_x < 0f && !is_facing_right) ||
            (move_x > 0f && is_facing_right))
            flip();

    }

    private bool is_grounded() {
        RaycastHit2D hit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, .5f, ground_layer);
        return hit.collider != null;
    }

    private bool is_platformed() {
        RaycastHit2D hit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, .5f, platform_layer);
        return hit.collider != null;
    }

    private IEnumerator fall_down() {
        collider.enabled = false;
        yield return new WaitForSeconds(0.6f);
        collider.enabled = true;
    }

    private void flip() {
        is_facing_right = !is_facing_right;
        Vector2 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
