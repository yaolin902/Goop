using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    [SerializeField]
    private float decay_time = 10f;

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 9)
            Destroy(this.gameObject);
    }

    void Start() {
        StartCoroutine(destory_self());
    }

    IEnumerator destory_self() {
        yield return new WaitForSeconds(decay_time);
        Destroy(this.gameObject);
    }
}
