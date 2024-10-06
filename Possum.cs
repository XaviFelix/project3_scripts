/***************************************************************
*file: Possum.cs
*author: Marie Philavong & Xavier Felix
*class: CS 4700 � Game Development
*assignment: Program 3
*date last modified: 10/05/24
*
*purpose: This program controls the behavior of the possum enemy 
*         in the game, including its movement and attack behavior.
*
****************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possum : MonoBehaviour
{
    private Animator bossAnimator; 
    private Rigidbody2D rb; 
    private Transform player; 

    private int currentHealth; // current health
    public int maxHealth = 15; // max health of the boss
    public float movementSpeed = 2.0f; // movement speed
    public float attackCooldown = 6.0f; // time between attacks
    public float attackRange = 10.0f; // distance when boss will attack player
    private float nextAttackTime; // when the boss can attack again

    public GameObject garbagePrefab; // prefab for the garbage thrown by the boss
    public Transform garbageSpawnPoint;

    // function: Start
    // purpose: called before the first frame update. it initializes the possum's 
    //          health and finds references to the player's transform, Animator, and Rigidbody2D
    //          components at the start of the game.
    void Start()
    {
        currentHealth = maxHealth; // initialize health
        player = GameObject.FindGameObjectWithTag("Player").transform; // find the player
        bossAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // function: Update
    // purpose: called once per frame. it continuously moves the possum towards the
    //          player and checks for attack eligibility.
    void Update()
    {
        Move();
        CheckAttack();
    }

    // function: Move
    // purpose: moves the boss towards the player if they are outside of the attack range,
    //          and updates the boss's movement animation
    void Move()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // move the boss towards the player if outside attack range
        if(distanceToPlayer > attackRange)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.position, movementSpeed * Time.deltaTime);

            if(bossAnimator != null)
            {
                bossAnimator.SetBool("isMoving", true); // set moving animation

                // adjust boss's facing direction based on movement
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
            if(bossAnimator != null)
            {
                bossAnimator.SetBool("isMoving", false); // set idle animation
            }
        }
    }

    // function: CheckAttack
    // purpose: determines if the boss can attack based on the distance to the player and attack cooldown
    void CheckAttack()
    {
        if(player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if(distanceToPlayer <= attackRange && Time.time >= nextAttackTime)
            {
                Debug.Log("Boss is preparing to attack!");
                StartCoroutine(ThrowGarbage()); // start attacking player
                nextAttackTime = Time.time + attackCooldown; // reset attack cooldown
            }
        }
    }

    // function: ThrowGarbage (Coroutine)
    // purpose: instantiates and launches a garbage projectile towards the player
    IEnumerator ThrowGarbage()
    {
        // countdown before possum attacks again
        for (int i = 6; i > 0; i--)
        {
            Debug.Log($"Boss will attack in {i}...");
            yield return new WaitForSeconds(1);
        }

        GameObject garbage = Instantiate(garbagePrefab, garbageSpawnPoint.position, Quaternion.identity);
        Rigidbody2D rbGarbage = garbage.GetComponent<Rigidbody2D>();

        Vector2 directionToPlayer = (player.position - garbageSpawnPoint.position).normalized;
        float distanceToPlayer = Vector2.Distance(garbageSpawnPoint.position, player.position);

        float gravity = Mathf.Abs(Physics2D.gravity.y);
        float launchHeight = 2.0f; // set arc height
        float launchSpeed = Mathf.Sqrt(gravity * distanceToPlayer); // calculate launch speed

        rbGarbage.velocity = new Vector2(launchSpeed * directionToPlayer.x, launchHeight); // launch garbage

        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        garbage.transform.rotation = Quaternion.Euler(0, 0, angle); // rotate garbage to face the player

        yield return new WaitForSeconds(attackCooldown); // wait for the next attack cooldown
    }

    // function: TakeDamage
    // purpose: reduces the possum's health when taking damage, updates the health bar, and checks if the boss should die
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // reduce health

        /*
         * if(healthBar != null)
        {
            healthBar.fillAmount = (float)currentHealth / maxHealth; // update health bar
        }
        */

        Debug.Log("Possum current health: " + currentHealth);

        if(currentHealth <= 0)
        {
            Die(); // trigger death if health is zero
        }
    }

    // function: Die
    // purpose: handles boss death and destroys the Possum GameObject
    void Die()
    {
        Debug.Log("Boss defeated!");
        Destroy(gameObject);
    }
}