/***************************************************************
*file: Bullet.cs
*author: Xavier Felix & Marie Philavong
*class: CS 4700 - Game Development
*assignment: Program 3
*date last modified: 10/09/24
*
*purpose: This program controls the behavior of the bullet projectile
*         in the game, including its movement and collision detection.
*
****************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;

    private float speed = 20f; // speed of the bullet

    // function: Awake
    // purpose: called when the script instance is being loaded; initializes the
    //          Rigidbody2D component for the bullet and checks if it is properly assigned
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // ensure the Rigidbody2D component is assigned
        if(rb == null)
        {
            Debug.LogError("Rigidbody2D component is not attached to the bullet prefab.");
        }
    }

    // function: Start
    // purpose: called before the first frame update; sets the bullet's velocity to
    //          move forward immediately after it spawns
    void Start()
    {
        // set the bullet's velocity to move forward
        rb.velocity = transform.right * speed;
    }

    // function: OnCollisionEnter2D
    // purpose: handles collision detection for the bullet; applies damage to the possum
    //          on collision and destroys the bullet GameObject upon hitting the ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Possum"))
        {
            Possum possum = collision.gameObject.GetComponent<Possum>();

            if(possum != null)
            {
                possum.TakeDamage(1); // assuming the bullet deals 1 damage
            }

            // destroy the bullet upon collision with the possum
            Destroy(gameObject);
        }
        else if(collision.gameObject.CompareTag("Ground"))
        {
            // destroy the bullet upon collision with the ground
            Destroy(gameObject);
        }
    }
}
