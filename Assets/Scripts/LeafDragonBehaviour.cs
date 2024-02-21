using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LeafDragonBehaviour : CreatureBehaviour
{
    [SerializeField] float flySpeed = 4.0f;

    [SerializeField] private LayerMask obstacleCheckLayerMask;

    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
        direction = new Vector3(-0.4f, -1, 0);
    }

    float checkDistX;
    float checkDistY;

    public override void Start()
    {
        base.Start();
        checkDistX = transform.localScale.x / 2 + 0.5f;
        checkDistY = transform.localScale.y / 2 + 0.5f;
        StartCoroutine(WaitUntilPlayerNearby());
    }

    public override void Move()
    {
        if (bossFightStart && canMove && alive)
        {
            rb.velocity = direction * flySpeed;
        }
        
        // Check y axis
        if (Physics2D.Raycast(transform.position, (direction.y > 0 ? Vector2.up : Vector2.down), checkDistY, obstacleCheckLayerMask))
        {
            direction = new Vector3(direction.x, -direction.y, 0);
        }

        // Check x axis
        if (Physics2D.Raycast(transform.position, (direction.x > 0 ? Vector2.right : Vector2.left), checkDistX, obstacleCheckLayerMask))
        {
            Debug.Log("Hit side walls");
            direction = new Vector3(-direction.x, direction.y, 0);
            Flip();
        }   
    }

    public override void ResetCreature(PlayerRespawnEvent e)
    {
        // Only reset the boss if it is alive, other wise never reset it
        if (alive)
        {
            base.ResetCreature(e);
            Debug.Log("Initial position: " + initialPosition);
            Debug.Log("Dragon Resetted to " + transform.position);
            bossFightStart = false;
            StartCoroutine(WaitUntilPlayerNearby());
        }
    }

    IEnumerator WaitUntilPlayerNearby()
    {
        while (true)
        {
            yield return null;
            if ((PlayerController.Instance.transform.position -  transform.position).magnitude < 18)
            {
                bossFightStart = true;
                break;
            }
        }
    }

    bool bossFightStart = false;
}
