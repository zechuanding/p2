using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DryadBehaviour : CreatureBehaviour
{

    Rigidbody2D rb;
    Vector3 direction = Vector3.right;
    [SerializeField] float walkSpeed = 4.0f;

    [SerializeField] private LayerMask groundCheckLayerMask;
    [SerializeField] private LayerMask WallCheckLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            rb.velocity = new Vector2(direction.x * walkSpeed, rb.velocity.y);
        }

        if (Physics2D.Raycast(transform.position, direction, 0.55f, WallCheckLayerMask) || !Physics2D.Raycast(transform.position + direction * 0.5f, Vector3.down, 0.6f, groundCheckLayerMask))
        {
            direction = -direction;
        }
    }
}
