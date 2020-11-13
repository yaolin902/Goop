using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardShoot : MonoBehaviour
{
	// References what to shoot and at who
	[SerializeField] internal GameObject projectilePrefab;
	[SerializeField] internal Transform firePoint;
	[SerializeField] float bullet_speed;
	internal GameObject player;

	// Uses how many seconds in reload rate to determine how fast
	// the character shoots.
	internal float reloadRate = 5f;
	internal float reloadTime;
	
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
		reloadTime = reloadRate;
    }

    // Update is called once per frame
    void Update()
    {
		// Ticks away at the time before shooting, then restarting the timer
        reloadTime -= Time.deltaTime;
		if (reloadTime <= 0)
		{
			Shoot();
			reloadTime = reloadRate;
		}
    }
	
	void Shoot()
	{
		// Looks at player before creating bullet and firing at them from the firePointS
		//firePoint.transform.LookAt(player.transform);
		GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
		Rigidbody2D bullet_rb = bullet.GetComponent<Rigidbody2D>();

		// wizard shoots left or right
		float x_diff = this.transform.position.x - player.transform.position.x;
		if (x_diff >= 0)
			bullet_rb.velocity = new Vector2(-bullet_speed, bullet_rb.velocity.y);
		else
			bullet_rb.velocity = new Vector2(bullet_speed, bullet_rb.velocity.y);
	}
}
