/***************************************************************
*file: SquirrelController.cs
*author: Xavier Felix & Marie Philavong
*class: CS 4700 - Game Development
*assignment: Program 3
*date last modified: 10/09/24
*
*purpose: This program controls the behavior of the squirrel character,
*         including movement, jumping, animation, and health management.
*
****************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirrelController : MonoBehaviour
{
    // [SerializeField] - > Tells Unity that we can edit our attributes it in the Inspector

    [Header("Movement")]
    [SerializeField]
    private Rigidbody2D squirrelRigidBody2D;
    [SerializeField]
    private float maxVelocity = 5f; // maximum horizontal velocity
    [Space(10)]


    [Header("Jumping")]
    [SerializeField]
    private Transform bottomCollisionPoint = null; // point to check for ground collision
    [SerializeField]
    private LayerMask platformLayer; // layer for ground detection
    private float jumpForce = 12f; // force applied when jumping
    [SerializeField]
    private int maxJumpCount = 1; // maximum number of jumps
    private int jumpCount = 0; // current jump count
    [SerializeField]
    private bool isGround = true; // checks if the squirrel is on the ground
    [Space(10)]


    [Header("Animation")]
    [SerializeField]
    private SpriteRenderer squirrelSpriteRenderer = null;
    [SerializeField]
    private Animator squirrelAnimator = null;
    [Space(10)]

    [Header("Camera")]
    [SerializeField]
    private Transform cameraTransform = null;

    [Header("Health")]
    [SerializeField]
    public int maxHealth = 15; // max health of the squirrel
    private int currentHealth; // current health of the squirrel


    // function: Awake
    // purpose: called when the script instance is being loaded; initializes components for the squirrel
    private void Awake()
    {
        squirrelRigidBody2D = GetComponent<Rigidbody2D>();
        squirrelSpriteRenderer = GetComponent<SpriteRenderer>();
        squirrelAnimator = GetComponent<Animator>();
    }

    // function: Start
    // purpose: called before the first frame update; sets the current health to maximum
    //          health at the start of the game
    void Start()
    {
        currentHealth = maxHealth;
    }

    // function: Update
    // purpose: called once per frame; handles input for movement and jumping for each frame
    void Update()
    {
        // isGround = Physics2D.OverlapCircle(bottomCollisionPoint.position, .2f, platformLayer);

        // handle jumping
        if(Input.GetButtonDown("Jump") && jumpCount < maxJumpCount)
        {
            jumpCount++;
            isGround = false;
            squirrelRigidBody2D.velocity = new Vector2(squirrelRigidBody2D.velocity.x, jumpForce);
        }

        // reset jump count if grounded
        if(isGround == true && jumpCount > 0)
        {
            jumpCount = 0;
        }

        // update squirrel velocity based on horizontal input
        squirrelRigidBody2D.velocity = new Vector2(Input.GetAxis("Horizontal") * maxVelocity, squirrelRigidBody2D.velocity.y);
    }

    // function: LateUpdate
    // purpose: updates the squirrel's rotation, camera position, and animator parameters
    private void LateUpdate()
    {
        // rotate squirrel based on direction
        if(squirrelRigidBody2D.velocity.x < 0f)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f); // face left
        }
        else if (squirrelRigidBody2D.velocity.x > 0f)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f); // face right
        }

        // move the camera to follow the squirrel
        if(cameraTransform != null)
        {
            cameraTransform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                cameraTransform.position.z
                );
        }

        // update animation based on horizontal velocity
        if(squirrelAnimator != null)
        {
            squirrelAnimator.SetFloat("xVelocity", Mathf.Abs(squirrelRigidBody2D.velocity.x));
        }
    }

    private void FixedUpdate()
    {

    }

    // function: OnCollisionStay2D
    // purpose: checks for ground collision while in contact with another collider
    private void OnCollisionStay2D(Collision2D collision)
    {
        isGround = Physics2D.OverlapCircle(bottomCollisionPoint.position, .2f, platformLayer);
    }

    // function: OnCollisionExit2D
    // purpose: sets isGround to false when exiting a collision with another collider
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGround = false;
    }


    // function: TakeDamage
    // purpose: reduces the squirrel's health by the specified damage amount and checks for defeat
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // reduce damage

        Debug.Log("Hazel's current health: " + currentHealth); // prints current health

        if(currentHealth <= 0)
        {
            Die(); // trigger death if health is zero
        }
    }

    // function: Die
    // purpose: handles player death and destroys the Squirrel GameObject
    void Die()
    {
        Debug.Log("Hazel has been defeated!");
        Destroy(gameObject);
    }
}
