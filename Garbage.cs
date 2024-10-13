/***************************************************************
*file: Garbage.cs
*author: Marie Philavong & Xavier Felix
*class: CS 4700 - Game Development
*assignment: Program 3
*date last modified: 10/05/24
*
*purpose: This program controls the behavior of garbage projectiles 
*         in the game. It manages the lifetime of the garbage and destroys 
*         it upon collision with the player or the ground.
*
****************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage : MonoBehaviour
{
    private bool hasCollided = false; //tracks if garbage has collided with player
    private const int GARBAGE_DAMAGE = 5;

    // function: Start
    // purpose: called before the first frame update
    void Start()
    {

    }

    // function: OnCollisionEnter2D
    // purpose: this function is triggered when the garbage projectile collides with another object. 
    //          it checks if the object is the player or the ground and destroys the garbage upon contact.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // handle collision with the player
        if (collision.gameObject.CompareTag("Player") && !hasCollided)
        {
            hasCollided = true;
            SquirrelController hazel = collision.gameObject.GetComponent<SquirrelController>();

            // apply damage if player has been hit
            if (hazel != null)
            {
                hazel.TakeDamage(GARBAGE_DAMAGE);
                Debug.Log("Squirrel has been hit!"); // print to console when the player is hit
            }

            Destroy(gameObject); // destroy garbage after hitting the player
        }

        // handle collision with the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject); // destroy garbage upon ground impact
        }
    }
}
