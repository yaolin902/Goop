using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericHealth : MonoBehaviour
{
	[SerializeField] int health = 0;
	
	void takeDamage(int toDamage)
	{
		health -= toDamage;
		if (health <= 0) Destroy(gameObject);
	}
}
