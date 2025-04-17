using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Serialized Fields
    [SerializeField] float speed = 5f;
    [SerializeField] float yInitialVelo = 15f;
    [SerializeField] float yIncrementalVelo = 10f; // gains/loses this amount of velocity per second. Also acts as gravity for player
    [SerializeField] float maxJumpHeight = 20f;
    [SerializeField] float accelTime = 0.2f; // time in air player spends accelerating in seconds
    [SerializeField] float hoverCoef = 0.8f;
    [SerializeField] float yLowVelo = 2f;
    [SerializeField] float edgeJumpLienency = 0.3f; // time not on ground where player can still jump
    [SerializeField] float groundCheckDistance = 0.5f;

    JumpPhase curPhase = JumpPhase.Fall;

    float yVeloDirection = -1;
    float yPosInitial;
    float hoverMod;


    enum JumpPhase
    {
        Acceleration,
        Deceleration,
        Hover,
        Fall
    }

    // private fields
    private BoxCollider2D boxCol;
    private Vector2 velo = Vector2.zero;
    private Rigidbody2D rb;
    private float lienencyTime = 0;
    private bool isGrounded = false;
    private string platformTag = "platform"; // using "platform" in update creates garbage
    private string playerTag = "Player";
    private string horiz = "Horizontal";
    private float accelUntil = 0;


    // method that allows other scripts to reset movement
    public void resetMovement()
    {
        rb.linearVelocity = Vector2.zero;
        velo = Vector2.zero;
        isGrounded = false;
        lienencyTime = 0;
        curPhase = JumpPhase.Fall;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {

        // allow to still jump edgeJumpLienency seconds after walking off edge
        if (isGrounded)
        {
            lienencyTime = Time.time + edgeJumpLienency;
        }


        hoverMod = 1;
        velo.y = rb.linearVelocityY;

        // Detect jump input
        if (Input.GetKey(KeyCode.Space))
        {
            if (isGrounded || lienencyTime > Time.time)
            {
                isGrounded = false;
                velo.y = yInitialVelo; 
                rb.linearVelocityY = velo.y;
                curPhase = JumpPhase.Acceleration;
                yVeloDirection = 1;
                yPosInitial = transform.position.y;
                lienencyTime = 0;
                accelUntil = Time.time + accelTime;
            }
            else
            {
               
                if (curPhase != JumpPhase.Acceleration)
                {
                    yVeloDirection = -1;

                    if (curPhase == JumpPhase.Hover)
                    {
                        hoverMod = hoverCoef;
                    }
                }

                if (curPhase < JumpPhase.Hover && transform.position.y - yPosInitial > maxJumpHeight)
                {
                    curPhase = JumpPhase.Hover;
                }

                if (accelUntil < Time.time && curPhase == JumpPhase.Acceleration)
                {
                    curPhase = JumpPhase.Hover;
                }
                else if (velo.y <= 0) 
                {
                    if (curPhase == JumpPhase.Deceleration)
                    {
                        curPhase = JumpPhase.Hover;
                    }
                    else if (velo.y <= -yLowVelo)
                    {
                        curPhase = JumpPhase.Fall;
                    }

                }
            }
        }
        else
        {
            // not holding jump
            if (curPhase <= JumpPhase.Hover)
            {
                yVeloDirection = -1;
                if (curPhase == JumpPhase.Hover)
                {
                    curPhase = (velo.y <= -yLowVelo) ? JumpPhase.Fall : JumpPhase.Hover;
                    hoverMod = hoverCoef;
                }
                else
                {
                    curPhase = JumpPhase.Hover;
                }
            }
            else
            {
                hoverMod = 1;
                
            }
            if (velo.y > yLowVelo)
            {
                velo.y = yLowVelo;
            }


        }

        float xJumpVeloMod;
        if (isGrounded)
        {
            xJumpVeloMod = 1;
        }
        else
        {
            xJumpVeloMod = 0.75f;
        }
        velo.x = Input.GetAxisRaw(horiz) * speed * xJumpVeloMod;


    }

    private void FixedUpdate()
    {

        if (curPhase == JumpPhase.Acceleration && rb.linearVelocityY == 0)
        {
            curPhase = JumpPhase.Fall;
        }

        yVeloDirection = (curPhase == JumpPhase.Acceleration) ? 1 : -1;

        if (!isGrounded)
        {
            velo.y += yIncrementalVelo * yVeloDirection * hoverMod * Time.deltaTime;
        }
        rb.linearVelocity = velo;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == platformTag || col.gameObject.tag == playerTag)
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == platformTag || col.gameObject.tag == playerTag)
        {
            isGrounded = false;
        }
    }

  

    private void debugInfo()
    {
        Debug.Log("Velo: " + rb.linearVelocity
            + "\nGrounded: " + isGrounded
            + "\nPhase: " + curPhase
            + "\nYVDir: " + yVeloDirection
            );
        
    }


}
