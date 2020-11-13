using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 9)
            Destroy(this.gameObject);
    }
}
