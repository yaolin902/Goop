using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericHealth : MonoBehaviour
{
	public int health = 0;
	
	void takeDamage(int toDamage)
	{
		// Will always assume the damage is taken.
		// Works alongside GenericDamage.cs
		// and any script that can call the
		// "takeDamage" function.

		// player immune to damage when attacking
		if (this.gameObject == GameObject.FindWithTag("Player")) {
			if (this.gameObject.GetComponent<PlayerController>().is_attacking) {
				return;
			}
		}

		health -= toDamage;
		if (health <= 0) Destroy(gameObject);
	}
}
