using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicochetBulletColllision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 9 || collision.gameObject.layer == 12 || collision.gameObject.layer == 14)
        {
            Vector3 v = Vector3.Reflect(transform.up, collision.contacts[0].normal);
            float rot = 90 - Mathf.Atan2(v.z, v.x) * Mathf.Rad2Deg;
            transform.position = v;
            Debug.Log("Ricochet Collision");
        }
    }

}
