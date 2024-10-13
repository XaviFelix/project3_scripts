/***************************************************************
*file: CheeseBullet.cs
*author: Xavier Felix & Marie Philavong
*class: CS 4700 - Game Development
*assignment: Program 3
*date last modified: 10/12/24
*
*purpose: This program controls the behavior of the cheese 
*         projectile and allowis the Squirrel object to take damage 
*         upon collision.
*         
****************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseBullet : MonoBehaviour
{
    // constants
    private const int CHEESE_DAMAGE = 1; // damage the cheese projectile will do
    private const float ATTACK_COOLDOWN = 0.5f; // cooldown before next attack (in seconds)

    private SquirrelController player;
    private Rigidbody2D rb;

    // function: Start
    // purpose: called before the first frame update; initializes the cheese projectile's
    //          Rigidbody component, finds the player by calculating the direction towards
    //          them, and sets the projectile's velocity to shoot at an arc
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<SquirrelController>(); // get SquirrelController directly

        // calculate direction to player
        Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        // calculate launch speed based on distance and gravity
        float gravity = Mathf.Abs(Physics2D.gravity.y);
        float launchHeight = 2.0f; // set arc height
        float launchSpeed = Mathf.Sqrt(gravity * distanceToPlayer); // calculate launch speed

        // set velocity for arc
        rb.velocity = new Vector2(launchSpeed * directionToPlayer.x, launchHeight); // launch cheese projectile

        // rotate cheese projectile to face the direction of movement
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle); // rotate cheese projectile to face the player
    }

    // function: OnTriggerEnter2D
    // purpose: gets the Squirrel instance and ensures it is not null; Squirrel then calls
    //          its TakeDamage method when a collision is triggered from the cheese projectile
    //          which is destroyed upon collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SquirrelController hazel = collision.GetComponent<SquirrelController>();

        if(hazel != null)
        {
            hazel.TakeDamage(CHEESE_DAMAGE);
        }

        Debug.Log(collision.name + " got hit");
        Destroy(gameObject); // destroy cheese projectile upon collision
    }
}