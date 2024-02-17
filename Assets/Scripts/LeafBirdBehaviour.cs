using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LeafBirdBehaviour : CreatureBehaviour
{

    [SerializeField] float flySpeed = 4.0f;

    [SerializeField] private LayerMask obstacleCheckLayerMask;

    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
        direction = Vector3.up;
    }

    public override void Move()
    {
        if (canMove)
        {
            rb.velocity = new Vector2(rb.velocity.x, direction.y * flySpeed);
        }

        if (Physics2D.Raycast(transform.position, direction, 0.6f, obstacleCheckLayerMask))
        {
            direction = -direction;
        }
    }
}
