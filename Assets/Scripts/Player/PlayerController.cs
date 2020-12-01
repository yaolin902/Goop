using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float jump_force = 40f;
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float dash_attack_speed = 30f;
    [SerializeField]
    private float dash_attack_time = 0.15f;
    [SerializeField]
    private float dash_attack_cooldown = 1f;

    private float dash_attack_timer;

    [SerializeField]
    private LayerMask ground_layer;
    [SerializeField]
    private LayerMask platform_layer;

    private Rigidbody2D player;
    private GameObject projectile_player;
    private BoxCollider2D ground_collider;
    private Animator animator;

    private bool is_facing_right = true;
    [HideInInspector]
    public bool is_attacking = false;
    private void Start()
    {
        player = GetComponent<Rigidbody2D>();
        ground_collider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        projectile_player = GameObject.FindWithTag("Projectile Player");

        dash_attack_timer = dash_attack_cooldown + dash_attack_time;
    }
    private void FixedUpdate()
    {
        // player movement
        float move_x = Input.GetAxis("Horizontal");
        player.velocity = new Vector2(speed * move_x, player.velocity.y);
        animator.SetFloat("speed", Mathf.Abs(speed * move_x));

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
        if ((Input.GetKeyDown(KeyCode.Z) && (is_grounded() || is_platformed())) || is_attacking) {
            is_attacking = true;
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
        yield return new WaitForSeconds(0.3f);
        ground_collider.enabled = true;
    }

    private void flip() {
        is_facing_right = !is_facing_right;
        transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);

        // also flip projectile player direction
        projectile_player.SendMessage("flip");
    }

    private void straight_attack() {
        if (is_attacking) {
            if (dash_attack_timer <= 0) {
                is_attacking = false;
                dash_attack_timer = dash_attack_cooldown + dash_attack_time;
                player.velocity = Vector2.zero;
            } else if (dash_attack_timer <= dash_attack_cooldown) {
                dash_attack_timer -= Time.deltaTime;
                return;
            } else {
                dash_attack_timer -= Time.deltaTime;

                if (is_facing_right) {
                    player.velocity = Vector2.right * dash_attack_speed;
                } else {
                    player.velocity = Vector2.left * dash_attack_speed;
                }
            }
        }
    }
    
}
