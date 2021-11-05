using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    [SerializeField]
    private float decay_time = 10f;

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
    void Start() {
        StartCoroutine(destory_self());
    }

    IEnumerator destory_self() {
        yield return new WaitForSeconds(decay_time);
        Destroy(this.gameObject);
    }
}
