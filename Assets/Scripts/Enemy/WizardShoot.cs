using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardShoot : MonoBehaviour
{
	// References what to shoot and at who
	[SerializeField] internal GameObject projectilePrefab;
	[SerializeField] internal GameObject firePoint;
	internal GameObject player;

	// Uses how many seconds in reload rate to determine how fast
	// the character shoots.
	internal float reloadRate = 5;
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
		firePoint.transform.LookAt(player.transform);
		Instantiate(projectilePrefab, firePoint.transform.position, firePoint.transform.rotation);
	}
}
