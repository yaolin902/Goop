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
		if (collision.gameObject.layer != 8 && collision.gameObject.layer != 9) {
			collision.gameObject.SendMessage("takeDamage", damage);
			StartCoroutine(damage_effect(this.GetComponent<SpriteRenderer>(), 0.5f));
		}
	}

	// damage effect is set to red
	IEnumerator damage_effect(SpriteRenderer sr, float effect_time) {
		Color original_color = sr.color;
		sr.color = Color.red;

		for (float t = 0; t < 1.0f; t += Time.deltaTime / effect_time) {
			sr.color = Color.Lerp(Color.red, original_color, t);
			yield return null;
		}

		sr.color = original_color;
	}
}
