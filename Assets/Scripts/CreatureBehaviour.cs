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


    public virtual void Start()
    {
        RecordInitialStatus();
        EventBus.Subscribe<PlayerRespawnEvent>(ResetCreature);
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
        
    }

    public virtual void Move() { }




    protected Vector3 initialPosition, initialDirection, initialScale;
    int initialHealth;
    protected void RecordInitialStatus()
    {
        initialPosition = transform.position;
        initialScale = transform.localScale;
        initialDirection = direction;
        if (ch != null)
        {
            initialHealth = ch.health;
        }
    }


    public virtual void ResetCreature(PlayerRespawnEvent e)
    {
        if (canRespawn)
        {
            transform.position = initialPosition;
            transform.localScale = initialScale;
            direction = initialDirection;
            sr.enabled = true;
            if (ch != null)
            {
                ch.health = initialHealth;
            }
            canMove = true;
            alive = true;
            rb.velocity = Vector2.zero;
        }
    }

    public void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
