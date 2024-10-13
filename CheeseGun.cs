/***************************************************************
*file: CheeseGun.cs
*author: Xavier Felix & Marie Philavong
*class: CS 4700 - Game Development
*assignment: Program 3
*date last modified: 10/11/24
*
*purpose: This program controls the mouse enemies projectile 
*         launcher (CheeseGun) that shoots cheese projectiles 
*         at the player when they are within a specified range. 
*         The CheeseGun calculates the player's distance and 
*         shoots after a cooldown period.
*         
****************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseGun : MonoBehaviour
{
    // constants
    private const float ATTACK_RANGE = 15.0f; // weapon attack range

    private Transform player; 
    private float timer; // controls time between shots

    public GameObject cheese; // cheese projectile
    public Transform cheesePoint; // cheese projectile spawn point

    // function: Start
    // purpose: called before the first frame update; finds and stores the player's
    //          Transform component by searching for an object tagged as "Player"
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // function: Update
    // purpose: called once per frame; continuously checks the distance between the
    //          CheeseGun and the player. if the player is within the specified attack
    //          range and the cooldown period has passed, a cheese projectile is shot.
    void Update()
    {
        if(player != null) // ensure player exists
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            timer += Time.deltaTime; // increment timer

            // shoot if cooldown has passed and player is within range
            if(timer > 3 && distanceToPlayer <= ATTACK_RANGE && distanceToPlayer > 1.0f)
            {
                timer = 0; // reset timer
                Shoot(); // shoot cheese projectile
            }
        }
    }

    // function: Shoot
    // purpose: instantiates and shoots a cheese projectile from the spawn location and rotation
    void Shoot()
    {
        Instantiate(cheese, cheesePoint.position, cheesePoint.rotation);
    }
}