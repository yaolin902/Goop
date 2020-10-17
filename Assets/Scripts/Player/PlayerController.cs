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
    private float attack_range = 7f;
    [SerializeField]
    private float attack_cooldown = 1.5f;

    [SerializeField]
    private LayerMask ground_layer;
    [SerializeField]
    private LayerMask platform_layer;

    private Rigidbody2D player;
    private GameObject projectile_player;
    private BoxCollider2D ground_collider;

    private bool is_facing_right = true;
    private bool is_attacking = false;
    private void Start()
    {
        player = GetComponent<Rigidbody2D>();
        ground_collider = GetComponent<BoxCollider2D>();
        projectile_player = GameObject.FindWithTag("Projectile Player");
    }
    private void FixedUpdate()
    {
        // player movement
        float move_x = Input.GetAxis("Horizontal");
        player.velocity = new Vector2(speed * move_x, player.velocity.y);

        // jump
        if (Input.GetKeyDown(KeyCode.Space) && (is_grounded() || is_platformed()))
            player.velocity = new Vector2(player.velocity.x, jump_force);

        // fall through platform
        if (Input.GetKeyDown(KeyCode.DownArrow) && is_platformed())
            StartCoroutine(fall_down());

        // player direction
        if ((move_x < 0f && is_facing_right) ||
            (move_x > 0f && !is_facing_right))
            flip();

        // player straight attack
        if (Input.GetKeyDown(KeyCode.Z)) {
            straight_attack();
        }

    }

    // ground checks
    private bool is_grounded() {
        RaycastHit2D hit = Physics2D.BoxCast(ground_collider.bounds.center, ground_collider.bounds.size, 0f, Vector2.down, .5f, ground_layer);
        return hit.collider != null;
    }

    private bool is_platformed() {
        RaycastHit2D hit = Physics2D.BoxCast(ground_collider.bounds.center, ground_collider.bounds.size, 0f, Vector2.down, .5f, platform_layer);
        return hit.collider != null;
    }

    private IEnumerator fall_down() {
        ground_collider.enabled = false;
        yield return new WaitForSeconds(0.6f);
        ground_collider.enabled = true;
    }

    private void flip() {
        is_facing_right = !is_facing_right;
        transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);

        // also flip projectile player direction
        projectile_player.SendMessage("flip");
    }

    private void straight_attack() {
        
    }
    
}
