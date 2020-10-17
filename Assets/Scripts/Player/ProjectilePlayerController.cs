using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePlayerController : MonoBehaviour
{
    private GameObject player;

    [SerializeField]
    private float player_distance = 2f;

    [SerializeField]
    private float speed = 10f;

    private void Start() {
        player = GameObject.FindWithTag("Player");
    }

    private void FixedUpdate() {
        // move towards to player
        if (Vector2.Distance(this.transform.position, player.transform.position) > player_distance) {
            this.transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    private void flip() {
        transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
    }

    /* Alyssa's projectile attack {WIP}
    // FirePoint
    public Transform firePoint;
    public GameObject gaapPrefab;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.X))
        {
            Shoot();
        }
        
    }

    // Shoot
    void Shoot()
    {
        // spawn
        Instantiate(gaapPrefab, firePoint.position, firePoint.rotation);
    }
    */
}
