using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Yarnball : MonoBehaviour
{
    private GameObject player;
    private Vector3 starting_pos;
    private Animator animator;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float freeze_time = 1f;

    private void Start() {
        player = GameObject.FindWithTag("Player");
        GetComponent<AIDestinationSetter>().target = player.transform;
        animator = GetComponent<Animator>();
        starting_pos = transform.position;
    }

    private void FixedUpdate() {
        // move towards to player
        // if (Vector2.Distance(this.transform.position, player.transform.position) != 0f) {
             // this.transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);

             // look towards player
             // float offset = 180f;
             // Vector2 direction = (Vector2)player.transform.position - (Vector2)transform.position;
             // direction.Normalize();
             // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
             // transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
        // }

        // freeze object movement
        freeze_time -= Time.deltaTime;

        if (freeze_time <= 0f) {
            animator.SetTrigger("idle");
        } else {
            transform.position = starting_pos;
        }
    }
}
