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
		
		collision.gameObject.SendMessage("takeDamage", damage);
	}
}
