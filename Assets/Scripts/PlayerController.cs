using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /* TO DO:
     * 
     * Consider adding coyote jump and jump input buffer
     */
    private Rigidbody2D rb;

    [Header("Horizontal Movement Settings")]
    [SerializeField] private float walkSpeed = 1;
    private float xAxis;

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 45;
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float groundCheckDist = 0.2f;
    [SerializeField] private float groundCheckXPos = 0.5f;
    [SerializeField] private LayerMask jumpCheckLayerMask;

    public static PlayerController Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        Move();
        Jump();
    }

    void GetInputs()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
    }

    void Move()
    {
        rb.velocity = new Vector2(walkSpeed * xAxis, rb.velocity.y);
    }

    // Check Player on the Ground
    public bool Grounded()
    {
        if (Physics2D.Raycast(groundCheckPoint.position, Vector2.down, groundCheckDist, jumpCheckLayerMask)
            || Physics2D.Raycast(groundCheckPoint.position + new Vector3(groundCheckXPos, 0, 0), Vector2.down, groundCheckDist, jumpCheckLayerMask)
            || Physics2D.Raycast(groundCheckPoint.position + new Vector3(-groundCheckXPos, 0, 0), Vector2.down, groundCheckDist, jumpCheckLayerMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Jump()
    {
        // Cancel Jump when button released
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Z))
        {
            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z))
        {
            if (Grounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
    }
}
