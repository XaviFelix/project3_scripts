/***************************************************************
*file: EdgeDetector.cs
*author: Marie Philavong & Xavier Felix
*class: CS 4700 – Game Development
*assignment: Program 3
*date last modified: 10/04/24
*
*purpose: This program handles edge detection for the possum. 
*         When the possum reaches the edge of a platform, it jumps to 
*         prevent falling off. The script applies a jump force whenever 
*         the edge is detected and ensures the possum is grounded before jumping.
*
****************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeDetector : MonoBehaviour
{
    public float jumpForce = 3f;
    public LayerMask groundLayer;
    public float detectionDistance = 1f;
    private Rigidbody2D rb;

    // function: Start
    // purpose: called before the first frame update. it initializes the Rigidbody2D component
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // function: Update
    // purpose: called once per frame. it calls the edge detection method in every frame
    //          to ensure continuous checking of whether the possum is at the edge.
    void Update()
    {
        DetectEdge();
    }

    // function: DetectEdge
    // purpose: casts a ray downwards to detect the ground. if no ground
    //          is detected, it calls the Jump method to make the possum jump.
    void DetectEdge()
    {
        // raycast downwards to check for ground/platform
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, detectionDistance, groundLayer);

        // check if there is no ground detected
        if(hit.collider == null)
        {
            // make the possum jump
            Jump();
        }
    }

    // function: Jump
    // purpose: applies an upward force to the possum if it is grounded, making it jump
    void Jump()
    {
        if(IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // apply jump force
        }
    }

    // function: IsGrounded
    // purpose: checks if the possum is grounded by casting a ray downward and
    //          returns true if it detects the ground, otherwise returns false
    bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, detectionDistance, groundLayer);
    }
}