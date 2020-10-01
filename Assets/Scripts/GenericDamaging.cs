using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericDamaging : MonoBehaviour
{
	[SerializeField] internal int damage = 0;

	void OnCollisionEnter2D(Collision2D collision)
	{
		collision.gameObject.SendMessage("takeDamage", damage);
	}
}
