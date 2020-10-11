using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePlayerController : MonoBehaviour
{
    private GameObject player;

    [SerializeField]
    private float player_distance = 2f;

    [SerializeField]
    private float speed = 4f;

    private void Start() {
        player = GameObject.FindWithTag("Player");
    }

    private void FixedUpdate() {
        // pathfind to player
        if (Vector2.Distance(this.transform.position, player.transform.position) > player_distance) {
            this.transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    private void flip() {
        transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
    }
}
