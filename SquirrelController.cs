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
    private float maxVelocity = 5f;
    [Space(10)]


    [Header("Jumping")]
    [SerializeField]
    private Transform bottomCollisionPoint = null;
    [SerializeField]
    private LayerMask platformLayer;
    private float jumpForce = 12f;
    [SerializeField]
    private int maxJumpCount = 1;
    private int jumpCount = 0;
    [SerializeField]
    private bool isGround = true;
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


    private void Awake()
    {
        squirrelRigidBody2D = GetComponent<Rigidbody2D>();
        squirrelSpriteRenderer = GetComponent<SpriteRenderer>();
        squirrelAnimator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // isGround = Physics2D.OverlapCircle(bottomCollisionPoint.position, .2f, platformLayer);

        if(Input.GetButtonDown("Jump") && jumpCount < maxJumpCount)
        {
            jumpCount++;
            isGround = false;
            squirrelRigidBody2D.velocity = new Vector2(squirrelRigidBody2D.velocity.x, jumpForce);
        }

        if(isGround == true && jumpCount > 0)
        {
            jumpCount = 0;
        }

        squirrelRigidBody2D.velocity = new Vector2(Input.GetAxis("Horizontal") * maxVelocity, squirrelRigidBody2D.velocity.y);

    }

    private void LateUpdate()
    {
        if(squirrelRigidBody2D.velocity.x < 0f)
        {
            // squirrelSpriteRenderer.flipX = true;
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if(squirrelRigidBody2D.velocity.x > 0f)
        {
            // squirrelSpriteRenderer.flipX = false;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

        // Camera works well in late update because we want everything to process first before moving camera
        if(cameraTransform != null)
        {
            cameraTransform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                cameraTransform.position.z
                );
        }

        if(squirrelAnimator != null)
        {
            squirrelAnimator.SetFloat("xVelocity", Mathf.Abs(squirrelRigidBody2D.velocity.x));
        }
    }

    private void FixedUpdate()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        isGround = Physics2D.OverlapCircle(bottomCollisionPoint.position, .2f, platformLayer);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGround = false;
    }
}
