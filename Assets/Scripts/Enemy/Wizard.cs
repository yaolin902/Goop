using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
	// // References what to shoot and at who
	// [SerializeField] internal GameObject projectilePrefab;
	// [SerializeField] internal Transform firePoint;
	// [SerializeField] float bullet_speed;
	// internal GameObject player;
	// private Animator animator;

	// // Uses how many seconds in reload rate to determine how fast
	// // the character shoots.
	// internal float reloadRate = 5f;
	// internal float reloadTime;
	
    // // Start is called before the first frame update
    // void Start()
    // {
    //     player = GameObject.FindWithTag("Player");
	// 	animator = GetComponent<Animator>();
	// 	reloadTime = reloadRate;
    // }

    // // Update is called once per frame
    // void Update()
    // {
	// 	// Ticks away at the time before shooting, then restarting the timer
    //     reloadTime -= Time.deltaTime;
	// 	if (reloadTime <= 1 && reloadTime >= 0.9f) {
	// 		// starts animation early
	// 		animator.SetBool("is_attack", true);
	// 	}
	// 	else if (reloadTime <= 0)
	// 	{
	// 		Shoot();
	// 		reloadTime = reloadRate;
	// 	}
    // }
	
	// void Shoot()
	// {
	// 	// Looks at player before creating bullet and firing at them from the firePointS
	// 	//firePoint.transform.LookAt(player.transform);
	// 	GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
	// 	Rigidbody2D bullet_rb = bullet.GetComponent<Rigidbody2D>();

	// 	animator.SetBool("is_attack", false);

	// 	// wizard shoots left or right
	// 	float x_diff = this.transform.position.x - player.transform.position.x;
	// 	if (x_diff >= 0)
	// 		bullet_rb.velocity = new Vector2(-bullet_speed, bullet_rb.velocity.y);
	// 	else
	// 		bullet_rb.velocity = new Vector2(bullet_speed, bullet_rb.velocity.y);
	// }

	// [SerializeField] internal float moveSpeed = 5;
	// [SerializeField] internal float desiredDistance = 2f;
	// private bool is_facing_right = false;

	// internal Rigidbody2D rigidBody;
	// internal GameObject player;
	
    // // Start is called before the first frame update
    // void Start()
    // {
    //     player = GameObject.FindWithTag("Player");
	// 	rigidBody = GetComponent<Rigidbody2D>();
    // }

    // // Update is called once per frame
    // void Update()
    // {
	// 	// This stores the distance from the current object and the player. This updates every frame.
	// 	float currentDistance = Vector2.Distance(player.transform.position, this.transform.position);
	// 	float x_diff = player.transform.position.x - this.transform.position.x;

	// 	if ((x_diff < 0f && is_facing_right) ||
    //         (x_diff > 0f && !is_facing_right))
    //         flip();
		
	// 	// This will try to keep the player in the desired distance by moving towards or away.
    //     if(currentDistance <= desiredDistance)
	// 		rigidBody.velocity = new Vector2(moveSpeed * Mathf.Sign(x_diff), 0);
	// 	else
	// 		rigidBody.velocity = new Vector2(0,0);
    // }

	// private void flip() {
    //     is_facing_right = !is_facing_right;
    //     transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
    // }

	// refactored code:
	public enum States {
		Idle,
		Moving
	}

	public enum AttackStates {
		Attacking,
		AttackCooldown,
		AttackReady
	}

	// wizard
	[SerializeField] private float bullet_speed;
	[SerializeField] private float move_speed;
	[SerializeField] private float desired_distance;
	[SerializeField] private float shoot_cooldown;
	private bool is_facing_right;
	private float shoot_duration;
	private float shoot_timer;
	[HideInInspector] public States wizard_states;
	[HideInInspector] public AttackStates wizard_attack_states;

	private Rigidbody2D wizard_rb;
	[SerializeField] private GameObject enemy_bullet;
	private GameObject player;
	private Animator wizard_animator;


	private void Start() {
		// default wizard attribute
		wizard_states = States.Idle;
		wizard_attack_states = AttackStates.AttackReady;
		move_speed = 4f;
		desired_distance = 4f;
		bullet_speed = 3f;
		is_facing_right = false;
		shoot_cooldown = 5f;
		shoot_duration = 1f;
		shoot_timer = shoot_duration;

		// unity object init
		player = GameObject.FindWithTag("Player");
		wizard_rb = GetComponent<Rigidbody2D>();
		wizard_animator = GetComponent<Animator>();
	}

	private void Update() {
		// wizard finite state machine
		// changing states
		float distance_from_player = Vector2.Distance(player.transform.position, this.transform.position);
		float x_diff = player.transform.position.x - this.transform.position.x;
		if (distance_from_player <= desired_distance && wizard_attack_states != AttackStates.Attacking) {
			wizard_states = States.Moving;
		} else if (distance_from_player >= desired_distance && wizard_states == States.Moving) {
			wizard_states = States.Idle;
		}

		// state function
		if (wizard_states == States.Idle) {
			wizard_rb.velocity = new Vector2(0,0);
			if ((x_diff < 0f && is_facing_right) || (x_diff > 0f && !is_facing_right))
				flip();
		}
		if (wizard_states == States.Moving) {
			if (Mathf.Abs(x_diff) >= 0.01f)
				wizard_rb.velocity = new Vector2(move_speed * Mathf.Sign(x_diff), 0);

			if ((x_diff < 0f && is_facing_right) || (x_diff > 0f && !is_facing_right) && Mathf.Abs(x_diff) <= 0.001f)
				flip();
		}

		// wizard attack finite state machine
		// changing states
		if (wizard_attack_states == AttackStates.AttackReady) {
			wizard_attack_states = AttackStates.Attacking;
			wizard_states = States.Idle;
		}

		// state function
		if (wizard_attack_states == AttackStates.Attacking) {
			if (shoot_projectile())
				StartCoroutine(attack_cooldown(shoot_cooldown));
		}
	}

	private void flip() {
        is_facing_right = !is_facing_right;
        transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
    }

	private bool shoot_projectile() {
		bool done_shooting = false;
		// Ticks away at the time before shooting, then restarting the timer
		if (shoot_timer <= 0) {
			// Looks at player before creating bullet and firing at them from the firePointS
			//firePoint.transform.LookAt(player.transform);
			GameObject bullet = Instantiate(enemy_bullet, transform.position, transform.rotation);
			Rigidbody2D bullet_rb = bullet.GetComponent<Rigidbody2D>();

			wizard_animator.SetBool("is_attack", false);
			done_shooting = true;

			// wizard shoots left or right
			float x_diff = player.transform.position.x - this.transform.position.x;
			if (x_diff <= 0f)
				bullet_rb.velocity = new Vector2(-bullet_speed, bullet_rb.velocity.y);
			else
				bullet_rb.velocity = new Vector2(bullet_speed, bullet_rb.velocity.y);

			shoot_timer = shoot_duration;
		} else {
			if (shoot_timer == 1f) {
				// starts animation early
				wizard_animator.SetBool("is_attack", true);
			}
			shoot_timer -= Time.deltaTime;
		}

		return done_shooting;
	}

	private IEnumerator attack_cooldown(float time) {
        wizard_attack_states = AttackStates.AttackCooldown;
        yield return new WaitForSeconds(time);
        wizard_attack_states = AttackStates.AttackReady;
    }
}
