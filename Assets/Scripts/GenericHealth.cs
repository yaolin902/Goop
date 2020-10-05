using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericHealth : MonoBehaviour
{
	[SerializeField] int health = 0;
	
	void takeDamage(int toDamage)
	{
		// Will always assume the damage is taken.
		// Works alongside GenericDamage.cs
		// and any script that can call the
		// "takeDamage" function.
		health -= toDamage;
		if (health <= 0) Destroy(gameObject);
	}
}
