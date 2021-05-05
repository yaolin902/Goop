using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum States {
        Idle,
        Walking,
        Jumping,
        InAir,
        Falling
    }

    public enum AttackStates {
        None,
        DashAttack,
        ProjectileAttack,
        AttackCooldown
    }

    // player
    [SerializeField] private float player_jump_force;
    [SerializeField] private float player_speed;
    [SerializeField] private float player_dash_attack_speed;
    [SerializeField] private float player_dash_attack_duration;
    [SerializeField] private float player_dash_attack_cooldown;
    [SerializeField] private float max_player_distance;
    [SerializeField] private float projectile_player_speed;
    [SerializeField] private float bullet_speed;
    [SerializeField] private float player_projectile_attack_cooldown;

    [HideInInspector] public States player_state;
    [HideInInspector] public AttackStates player_attack_state;
    private float dash_attack_timer;
    [HideInInspector] public bool is_player_facing_right;
    private bool can_player_shoot_sfx;

    // unity object
    [SerializeField] private LayerMask ground_layer;
    [SerializeField] private LayerMask platform_layer;
    [SerializeField] private GameObject bullet_sprite;

    private GameObject player;
    private GameObject projectile_player;
    private Rigidbody2D player_rb;
    private Animator player_animator;
    private BoxCollider2D player_collider;
    public GameObject cooldown_bar;
    
    private void Start() {
        // default player attribute
        player_state = States.Idle;
        player_attack_state = AttackStates.None;
        player_jump_force = 20f;
        player_speed = 10f;
        player_dash_attack_speed = 30f;
        player_dash_attack_duration = 0.15f;
        player_dash_attack_cooldown = 1f;
        is_player_facing_right = true;
        can_player_shoot_sfx = true;
        max_player_distance = 1.25f;
        projectile_player_speed = 2.5f;
        bullet_speed = 7f;
        player_projectile_attack_cooldown = 1.5f;
        dash_attack_timer = player_dash_attack_duration;

        // unity object init
        player = GameObject.FindWithTag("Player");
        projectile_player = GameObject.FindWithTag("Projectile Player");
        player_rb = player.GetComponent<Rigidbody2D>();
        player_collider = player.GetComponent<BoxCollider2D>();
        player_animator = player.GetComponent<Animator>();
    }

    private void FixedUpdate() {
        // test
        if (Input.GetKeyDown(KeyCode.T)) {
            player.transform.Translate(new Vector3(10f, 10f, 0f) * Time.deltaTime, Space.Self);
        }

        // player movement finite state machine
        // changing state
        //Debug.Log(player_state);
        if (player_state == States.Idle && Input.GetAxis("Horizontal") != 0) {
            player_state = States.Walking;
        } else if ((player_state == States.Idle || player_state == States.Walking || player_state == States.InAir)) {
            if (Input.GetKeyDown(KeyCode.Space) && player_state != States.InAir) {
                player_state = States.Jumping;
            } else if (Input.GetKeyDown(KeyCode.DownArrow) && is_player_on_platform()) {
                StartCoroutine(fall_down());
                player_state = States.Falling;
            } else if (player_rb.velocity.y < -0.1f) {
                player_state = States.Falling;
            } else if (player_rb.velocity.y > 0.1f) {
                player_state = States.InAir;
            }
        } else if (player_state == States.Falling && (is_player_on_platform() || is_player_on_ground())) {
            player_state = States.Idle;
        }

        // state function
        if (player_state != States.Idle) {
            move_player(Input.GetAxis("Horizontal"));
            move_projectile_player();
        }
        if (player_state == States.Jumping) {
            player_rb.velocity = new Vector2(player_rb.velocity.x, player_jump_force);
            player_animator.SetTrigger("jump");
            player_state = States.Falling;
        }
        if (player_state == States.Idle) {
            move_projectile_player();
        }

        
        // player attack finite state machine
        // changing state
        //Debug.Log(player_attack_state);
        if (player_attack_state == AttackStates.None && Input.GetKeyDown(KeyCode.Z) &&
            player_state != States.Jumping && player_state != States.Falling) {
            player_attack_state = AttackStates.DashAttack;
        } else if (player_attack_state == AttackStates.None && Input.GetKeyDown(KeyCode.X)) {
            player_attack_state = AttackStates.ProjectileAttack;
        }

        // state function
        if (player_attack_state == AttackStates.DashAttack) {
            if (can_player_shoot_sfx) {
                SFXController.Instance.play("player_dash");
                can_player_shoot_sfx = false;
            }
            if (dash_attack())
                StartCoroutine(attack_cooldown(player_dash_attack_cooldown));
        }
        if (player_attack_state == AttackStates.ProjectileAttack) {
            shoot_projectile();
            StartCoroutine(attack_cooldown(player_projectile_attack_cooldown));
        }
    }

    // player function
    private void move_player(float move_x) {
        player_rb.velocity = new Vector2(player_speed * move_x, player_rb.velocity.y);
        player_animator.SetFloat("speed", Mathf.Abs(player_speed * move_x));

        if ((move_x < 0f && is_player_facing_right) || (move_x > 0f && !is_player_facing_right))
            flip_player_sprite();
    }

    private void move_projectile_player() {
        // projectile player follows movable player
        if (Vector2.Distance(projectile_player.transform.position, player.transform.position) > max_player_distance) {
            // projectile_player.transform.position = Vector2.MoveTowards(projectile_player.transform.position, player.transform.position, player_speed * Time.deltaTime);
            // smoothier follow
            projectile_player.transform.position = Vector2.Lerp(projectile_player.transform.position, player.transform.position, projectile_player_speed * Time.deltaTime);
        }
    }
    private void flip_player_sprite() {
        is_player_facing_right = !is_player_facing_right;
        player.transform.localScale = new Vector2(-1 * player.transform.localScale.x, player.transform.localScale.y);
        projectile_player.transform.localScale = new Vector2(-1 * projectile_player.transform.localScale.x, projectile_player.transform.localScale.y);
    }

    private IEnumerator fall_down() {
        player_collider.enabled = false;
        yield return new WaitForSeconds(0.17f);
        player_collider.enabled = true;
    }

    private IEnumerator attack_cooldown(float time) {
        player_attack_state = AttackStates.AttackCooldown;
        cooldown_bar.SendMessage("set_max_value", time);
        yield return new WaitForSeconds(time);
        player_attack_state = AttackStates.None;
        can_player_shoot_sfx = true;
    }

    private bool is_player_on_ground() {
        RaycastHit2D hit = Physics2D.BoxCast(player_collider.bounds.center, player_collider.bounds.size, 0f, Vector2.down, .5f, ground_layer);
        return hit.collider != null;
    }

    private bool is_player_on_platform() {
        RaycastHit2D hit = Physics2D.BoxCast(player_collider.bounds.center, player_collider.bounds.size, 0f, Vector2.down, .5f, platform_layer);
        return hit.collider != null;
    }

    // attack functions
    private void shoot_projectile()
    {
        // spawn bullet
        GameObject bullet = Instantiate(bullet_sprite, projectile_player.transform.position, projectile_player.transform.rotation);
        Rigidbody2D bullet_rb = bullet.GetComponent<Rigidbody2D>();

        SFXController.Instance.play("player_shoot");

        // fire direction
        if (is_player_facing_right)
            bullet_rb.velocity = new Vector2(bullet_speed, bullet_rb.velocity.y);
        else
            bullet_rb.velocity = new Vector2(-bullet_speed, bullet_rb.velocity.y);
    }

    private bool dash_attack() {
        bool done_dashing = false;
        if (dash_attack_timer <= 0) {
            done_dashing = true;
            dash_attack_timer = player_dash_attack_duration;
            player_rb.velocity = Vector2.zero;
        } else {
            dash_attack_timer -= Time.deltaTime;

            if (is_player_facing_right) {
                player_rb.velocity = Vector2.right * player_dash_attack_speed;
            } else {
                player_rb.velocity = Vector2.left * player_dash_attack_speed;
            }
        }

        return done_dashing;
    }
}
