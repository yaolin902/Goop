using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yarnball : MonoBehaviour
{
    private GameObject player;
    [SerializeField]
    private float speed = 5f;

    private void Awake() {
        player = GameObject.FindWithTag("Player");
    }

    private void FixedUpdate() {
        // move towards to player
        if (Vector2.Distance(this.transform.position, player.transform.position) != 0f) {
            this.transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);

            // look towards player
            float offset = 180f;
            Vector2 direction = (Vector2)player.transform.position - (Vector2)transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
        }
    }
}
