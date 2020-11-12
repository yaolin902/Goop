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
    private bool is_facing_right = true;

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
        is_facing_right = !is_facing_right;
    }

    
    // FirePoint
    public Transform firePoint;
    public GameObject gaapPrefab;
    [SerializeField]
    private float bullet_speed = 5f;
    [SerializeField]
    private float bullet_despawn_radius = 10f;

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
        GameObject bullet = Instantiate(gaapPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bullet_rb = bullet.GetComponent<Rigidbody2D>();

        // fire direction
        if (is_facing_right)
            bullet_rb.velocity = new Vector2(bullet_speed, bullet_rb.velocity.y);
        else
            bullet_rb.velocity = new Vector2(-bullet_speed, bullet_rb.velocity.y);

        // despawn bullet
        // if (Vector2.Distance(bullet.transform.position, this.transform.position) >= bullet_despawn_radius)
        //     Destroy(bullet);
    }
    
}
