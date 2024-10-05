using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    public float speed = 20f;

    // Need a reference to its rigid body 
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        // Soon as bullet spawns, we tell it to fly forward
        rb.velocity = transform.right * speed;

    }

    
}
