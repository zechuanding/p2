using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    PlayerStateList ps;
    private Rigidbody2D rb;

    [Header("Horizontal Movement Settings")]
    [SerializeField] private float walkSpeed = 1;
    private float xAxis;

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 45;
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float groundCheckDist = 0.05f;
    [SerializeField] private float groundCheckXPos = 0.4f;
    [SerializeField] private LayerMask jumpCheckLayerMask;
    // Jump input buffer
    private float jumpBufferCounter = 0;
    [SerializeField] private float jumpBufferTime = 0.1f;
    // Coyote jump
    private float coyoteTimeCounter = 0;
    [SerializeField] private float coyoteTime = 0.2f;

    [SerializeField] private GameObject AuroraPlatformPrefab;

    public Vector3 facing = Vector3.right;

    private bool canMove = true;
    private int numMoveRestricts = 0;


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
        canMove = numMoveRestricts <= 0;
        GetInputs();
        UpdateJumpingState();
        Walk();
        Jump();
        CreatePlatform();
        Cheat();
    }

    // ================================== MOVE RESTRICTIONS ==================================
    public void AddMoveRestrict() { numMoveRestricts++; }
    public void RemoveMoveRestrict() { numMoveRestricts--; }
    public void ClearAllMoveRestrict() { numMoveRestricts = 0; }



    // ================================== WALK ==================================
    void GetInputs()
    {
        if (canMove)
        {
            xAxis = Input.GetAxisRaw("Horizontal");
            facing = xAxis > 0 ? Vector3.right : (xAxis < 0 ? Vector3.left : facing);
        }
        else
        {
            xAxis = 0;
        }
    }

    void Walk()
    {
        rb.velocity = new Vector2(walkSpeed * xAxis, rb.velocity.y);
    }


    // ================================== JUMP ==================================
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
        if (Input.GetButtonUp("Jump"))
        {
            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                coyoteTimeCounter = 0;
            }
        }

        if (!ps.jumping)
        {
            if (jumpBufferCounter > 0 && coyoteTimeCounter > 0)
            {
                Debug.Log("jumped with buffer count: " + jumpBufferCounter + " and coyoteTimeCounter: " + coyoteTimeCounter);
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                ps.jumping = true;
                jumpBufferCounter = 0;
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

        if (canMove && Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter--;
        }
    }

    // ================================== Platform ==================================
    public bool hasPlatformAbility = false;
    [SerializeField] private LayerMask groundLayerMask;
    void CreatePlatform()
    {
        if (canMove & hasPlatformAbility && Input.GetKeyDown(KeyCode.DownArrow) && !Physics2D.Raycast(transform.position, Vector2.down, 1.5f, groundLayerMask))
        {
            int platformCost = 25;
            if (PlayerStats.Instance.MP.Get() >= platformCost)
            {
                PlayerStats.Instance.MP.Add(-platformCost);
                Instantiate(AuroraPlatformPrefab, transform.position + new Vector3(0, -1f, 0), Quaternion.identity);
            } 
            else if (cheatMode)
            {
                Instantiate(AuroraPlatformPrefab, transform.position + new Vector3(0, -1f, 0), Quaternion.identity);
            }
        }
    }

    // ================================== CHEAT ==================================
    public bool cheatMode = false;
    void Cheat()
    {
        if (Input.GetKeyDown("1"))
        {
            cheatMode = !cheatMode;
            if (cheatMode)
            {
                PlayerStats.Instance.MP.Maximize();
            }
        }
    }


    // ================================== CHEAT ==================================
    


}
