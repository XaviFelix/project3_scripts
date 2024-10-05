using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform bulletPoint;

    // In order to reference the prefab
    public GameObject bulletPrefab;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        // Shooting logic

        // Whenever we want to spawn an object use Instantiate, 
        // we want the prefab to spawn at the bulletpoint position
        Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);

    }
}
