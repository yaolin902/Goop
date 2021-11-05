using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attach to objects that deal damage
// Works alongside GenericHealth.cs
// Along with any gameObject with a
// "takeDamage(int)" function.
public class GenericDamaging : MonoBehaviour
{
	[SerializeField] internal int damage = 0;
	[SerializeField] internal float knockback_force = 0f;

	private Rigidbody2D last_rb;
	private float last_collision;
	public float min_jump;
	public float min_wait;

	void OnTriggerEnter2D(Collider2D collision)
	{
		// When attached object collides with another,
		// it calls the "takeDamage" from any script attached to.
		// the collided object. It sends the "damage"
		// variable to the function.

		// ground layer: 8, one way platform layer: 9
		if (collision.gameObject.layer == 8 || collision.gameObject.layer == 9) {
			return;
		}

		// player immune to damage when dash attacking
		if (collision.gameObject == GameObject.FindWithTag("Player")) {
			PlayerController script = collision.gameObject.transform.parent.gameObject.GetComponent<PlayerController>();
			if (script.player_attack_state == PlayerController.AttackStates.DashAttack &&
				this.gameObject.layer == 12) { // enemy layer: 12
				return;
			}
		}

		collision.gameObject.SendMessage("takeDamage", damage);

		// take knockback
		if (this.gameObject == GameObject.FindWithTag("Player")) {
			Rigidbody2D rb = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
			PlayerController script = this.gameObject.transform.parent.gameObject.GetComponent<PlayerController>();
			if (script.player_attack_state == PlayerController.AttackStates.None || script.player_attack_state == PlayerController.AttackStates.AttackCooldown) {
				rb.AddForce(Vector2.up * min_jump, ForceMode2D.Impulse);
				last_rb = rb;
				last_collision = (collision.gameObject.transform.position - this.gameObject.transform.position).x;
				StartCoroutine(knockback_delay());
				// if ((collision.gameObject.transform.position - this.gameObject.transform.position).x < 0f)
				// 	rb.AddForce((Vector2.right + Vector2.up) * knockback_force, ForceMode2D.Impulse);
				// else
				// 	rb.AddForce((Vector2.left + Vector2.up) * knockback_force, ForceMode2D.Impulse);
			}
		}

		// play sfx
		if (collision.gameObject == GameObject.FindWithTag("Player")) {
			SFXController.Instance.play("player_damage");
		} else if (collision.gameObject == GameObject.FindWithTag("Enemy")) {
			SFXController.Instance.play("enemy_death");
		}
	}

	IEnumerator knockback_delay() {
		yield return new WaitForSeconds(min_wait);
		if (last_collision < 0f)
			last_rb.AddForce((Vector2.right + Vector2.up) * knockback_force, ForceMode2D.Impulse);
		else
			last_rb.AddForce((Vector2.left + Vector2.up) * knockback_force, ForceMode2D.Impulse);
	}
}
