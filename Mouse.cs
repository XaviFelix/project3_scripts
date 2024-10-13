/***************************************************************
*file: Mouse.cs
*author: Xavier Felix & Marie Philavong
*class: CS 4700 - Game Development
*assignment: Program 3
*date last modified: 10/05/24
*
*purpose: This program controls the behavior of a mouse enemy 
*         in the game. It manages the mouse's health, movement 
*         towards the player, and handles attack logic.
*
****************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    private Animator mouseAnimator;
    private Rigidbody2D rb;
    private Transform player;

    private int currentHealth; // current health
    public int maxHealth = 5; // max health of the mouse
    public float movementSpeed = 2.0f; // movement speed
    public float attackCooldown = 6.0f; // time between attacks
    public float attackRange = 15.0f; // distance when mouse will attack player
    private float nextAttackTime; // when the mouse can attack again


    // function: Start
    // purpose: called before the first frame update; initializes the mouse's 
    //          health and finds references to the player's transform
    void Start()
    {
        currentHealth = maxHealth; // initialize health
        player = GameObject.FindGameObjectWithTag("Player").transform; // find the player
        rb = GetComponent<Rigidbody2D>();
        mouseAnimator = GetComponent<Animator>(); 
    }

    // function: Update
    // purpose: called once per frame; continuously moves the mouse towards the player
    void Update()
    {
        Move();
    }

    // function: Move
    // purpose: moves the mouse towards the player only when the player is within the attack range.
    //          it also updates the mouse's movement animation and facing direction accordingly.
    //          if the player is outside the attack range, the boss remains idle.
    void Move()
    {
        if(player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // move the mouse towards the player only if they are within the attack range
            if(distanceToPlayer <= attackRange && distanceToPlayer > 1.0f)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                transform.position = Vector2.MoveTowards(transform.position, player.position, movementSpeed * Time.deltaTime);

                if(mouseAnimator != null)
                {
                    mouseAnimator.SetBool("isMoving", true); // set moving animation

                    // adjust mouse's facing direction based on movement
                    if(direction.x > 0)
                    {
                        transform.localScale = new Vector3(1, 1, 1); // face right
                    }
                    else if(direction.x < 0)
                    {
                        transform.localScale = new Vector3(-1, 1, 1); // face left
                    }
                }
            }
            else
            {
                if(mouseAnimator != null)
                {
                    mouseAnimator.SetBool("isMoving", false); // set idle animation
                }
            }
        }
    }

    // function: TakeDamage
    // purpose: reduces the mouse's health when taking damage, updates the health bar, and checks if the mouse should die
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // reduce health

        Debug.Log("Mouse current health: " + currentHealth); // prints mouse current health

        if(currentHealth <= 0)
        {
            Die(); // trigger death if health is zero
        }
    }

    // function: Die
    // purpose: handles mouse death and destroys the Mouse GameObject
    void Die()
    {
        Debug.Log("Mouse defeated!");
        Destroy(gameObject);
    }
}