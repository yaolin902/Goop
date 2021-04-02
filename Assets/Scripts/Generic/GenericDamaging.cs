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

	void OnCollisionEnter2D(Collision2D collision)
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
			rb.velocity = Vector3.zero;
			rb.AddForce(Vector2.one * knockback_force, ForceMode2D.Impulse);
		}

		// play sfx
		if (collision.gameObject == GameObject.FindWithTag("Player")) {
			SFXController.Instance.play("player_damage");
		} else if (collision.gameObject == GameObject.FindWithTag("Enemy")) {
			SFXController.Instance.play("enemy_death");
		}
	}
}
