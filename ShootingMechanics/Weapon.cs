/***************************************************************
*file: Weapon.cs
*author: Xavier Felix & Marie Philavong
*class: CS 4700 - Game Development
*assignment: Program 3
*date last modified: 10/05/24
*
*purpose: This program handles the shooting mechanics of the 
*         weapon, allowing the player to fire projectiles at the 
*         press of a button. It instantiates a bullet prefab at 
*         the specified spawn point when the fire button is pressed.
*
****************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform bulletPoint; // spawn point where bullets are fired
    public GameObject bulletPrefab; // reference to the bullet prefab

    // function: Start
    // purpose: called before the first frame update
    void Start()
    {

    }

    // function: Update
    // purpose: called once per frame; when the fire button is pressed, it
    //          triggers the Shoot method to instantiate a bullet at the spawn point
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    // function: Shoot
    // purpose: instantiates a bullet prefab at the bullet spawn point with 
    //          the specified rotation when the shoot action is triggered
    public void Shoot()
    {
        Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);
    }
}
