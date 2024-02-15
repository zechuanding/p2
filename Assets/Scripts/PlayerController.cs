using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /* TO DO:
     * 
     * Consider adding coyote jump and jump input buffer
     */
    PlayerStateList ps;
    private Rigidbody2D rb;

    [Header("Horizontal Movement Settings")]
    [SerializeField] private float walkSpeed = 1;
    private float xAxis;

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 45;
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float groundCheckDist = 0.1f;
    [SerializeField] private float groundCheckXPos = 0.4f;
    [SerializeField] private LayerMask jumpCheckLayerMask;
    // Jump input buffer
    private int jumpBufferCounter = 0;
    [SerializeField] private int bufferFrames = 6;
    // Coyote jump
    private float coyoteTimeCounter = 0;
    [SerializeField] private float coyoteTime = 0.2f;

    [SerializeField] private GameObject AuroraPlatformPrefab;


    // Singleton
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
        ps = GetComponent<PlayerStateList>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        UpdateJumpingState();
        Move();
        Jump();
        CreatePlatform();
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
    public bool PlayerOnGround()
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


    // Variable height jump
    void Jump()
    {
        // Cancel Jump when button released
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Z))
        {
            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                //ps.jumping = false;
            }
        }

        if (!ps.jumping)
        {
            if (jumpBufferCounter > 0 && coyoteTimeCounter > 0)
            {
                Debug.Log("jumped with buffer count: " + jumpBufferCounter + " and coyoteTimeCounter: " + coyoteTimeCounter);
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                ps.jumping = true;
            }
        }
    }

    void UpdateJumpingState()
    {
        if (PlayerOnGround())
        {
            ps.jumping = false;
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z))
        {
            jumpBufferCounter = bufferFrames;
        }
        else
        {
            jumpBufferCounter--;
        }
    }


    void CreatePlatform()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Instantiate(AuroraPlatformPrefab, transform.position + new Vector3(0, -1f, 0), Quaternion.identity);
        }
    }
}
