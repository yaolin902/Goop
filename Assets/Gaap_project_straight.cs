using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gaap_project_straight : MonoBehaviour
{
    // FirePoint
    public Transform FirePoint;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

    }

    void Shoot()
    {
        // shooting

    }
}
