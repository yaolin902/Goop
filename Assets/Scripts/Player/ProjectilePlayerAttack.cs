using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePlayerAttack : MonoBehaviour
{
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
}
