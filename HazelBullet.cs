/***************************************************************
*file: HazelBullet.cs
*author: Xavier Felix & Marie Philavong
*class: CS 4700 - Game Development
*assignment: Program 3
*date last modified: 10/05/24
*
*
*purpose: This program handles the behavior of a hazel bullet in 
*         the game, allowing it to move forward at a specified 
*         speed and deal damage to any enemies it collides with, 
*         such as mice and the possum boss.
*
****************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazelBullet : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed = 20f; // speed of the hazel bullet
    public int bulletDamage = 5; // damage dealt by the hazel bullet

    // function: Start
    // purpose: called before the first frame update; initializes the hazel
    //          bullet's velocity to make it fly forward as soon as it spawns
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed; // set the hazel bullet's velocity to fly forward
    }

    // function: OnTriggerEnter2D
    // purpose: detects collisions with other objects; if the hazel bullet collides
    //          with a mouse or the possum, it deals damage then destroys itself
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Possum bossPossum = collision.GetComponent<Possum>();
        Mouse enemyMouse = collision.GetComponent<Mouse>();

        if(enemyMouse != null)
        {
            enemyMouse.TakeDamage(bulletDamage); // apply damage to mouse
        }

        if(bossPossum != null)
        {
            bossPossum.TakeDamage(bulletDamage); // apply damage to possum
        }

        Debug.Log(collision.name + " got hit!");
        Destroy(gameObject);
    }
}
