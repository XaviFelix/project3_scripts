using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private bool isMoving;
    private Vector2 input;
    private Animator animator;

    // Awake is called when script instance is being loaded
    // As soon as player is loaded, this method runs
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check if user is pressing a key or not
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
            
            // Debug purposes
            Debug.Log("This is input.x: " + input.x);
            Debug.Log("This is input.y: " + input.y);

            

            // Removes diagonal movement
            if (input.x != 0) input.y = 0;

            // If input does not equal 0, (there is input)
            if(input != Vector2.zero)
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                // Whatever the position is and then incremet by what x and y are 
                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                StartCoroutine(Move(targetPos));
            }
        }

        animator.SetBool("isMoving", isMoving);
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        // IF there is any movement bigger than 0, that means the user  moved
        while((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon) 
        { 
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;
    }
}
