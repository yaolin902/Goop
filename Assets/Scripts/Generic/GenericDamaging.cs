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

	void OnCollisionEnter2D(Collision2D collision)
	{
		// When attached object collides with another,
		// it calls the "takeDamage" from any script attached to.
		// the collided object. It sends the "damage"
		// variable to the function.

		// ground layer: 8, one way platform layer: 9
		if (collision.gameObject.layer == 8 && collision.gameObject.layer == 9) {
			return;
		}

		// player immune to damage when attacking
		if (collision.gameObject == GameObject.FindWithTag("Player")) {
			if (collision.gameObject.GetComponent<PlayerController>().is_attacking &&
				this.gameObject.layer == 12) {
				return;
			}
		}

		collision.gameObject.SendMessage("takeDamage", damage);
	}
}
