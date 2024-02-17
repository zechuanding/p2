using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DryadBehaviour : CreatureBehaviour
{

    [SerializeField] float walkSpeed = 4.0f;

    [SerializeField] private LayerMask groundCheckLayerMask;
    [SerializeField] private LayerMask WallCheckLayerMask;

    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
        direction = Vector3.right;
    }

    public override void Move()
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
