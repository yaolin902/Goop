using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // FirePoint
    public Transform firePoint;
    public GameObject gaapPrefab;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
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
