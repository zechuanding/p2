using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureBehaviour : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected SpriteRenderer sr;
    protected Vector3 direction;
    public bool alive = true;
    public bool canRespawn = true;
    protected bool canMove = true;

    
    CreatureHealth ch;

    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        ch = GetComponent<CreatureHealth>();
    }


    protected void Start()
    {
        RecordInitialStatus();
        EventBus.Subscribe<PlayerRespawnEvent>(Respawn);
    }

    public virtual void Update()
    {
        if (alive)
        {
            Move();
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        sr.flipX = direction.x < 0;
    }

    public virtual void Move() { }




    Vector3 initialPosition, initialDirection;
    int initialHealth;
    protected void RecordInitialStatus()
    {
        initialPosition = transform.position;
        initialDirection = direction;
        if (ch != null)
        {
            initialHealth = ch.health;
        }
    }


    public virtual void Respawn(PlayerRespawnEvent e)
    {
        if (canRespawn)
        {
            transform.position = initialPosition;
            direction = initialDirection;
            sr.enabled = true;
            if (ch != null)
            {
                ch.health = initialHealth;
            }
            canMove = true;
            alive = true;
        }
    }
}
