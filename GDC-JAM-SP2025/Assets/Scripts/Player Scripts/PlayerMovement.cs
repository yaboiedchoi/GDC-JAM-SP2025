using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Serialized Fields
    [SerializeField] float speed = 5f;
    [SerializeField] float yInitialVelo = 15f;
    [SerializeField] float yIncrementalVelo = 10f; // gains/loses this amount of velocity per second. Also acts as gravity for player
    [SerializeField] float maxJumpHeight = 20f;
    [SerializeField] float maxYVelo = 8f;
    [SerializeField] float hoverCoef = 0.8f;
    [SerializeField] float yLowVelo = 2f;
    [SerializeField] float edgeJumpLienency = 0.3f; // time not on ground where player can still jump
    [SerializeField] float groundCheckDistance = 0.5f;
    [SerializeField] GameObject test;

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
    private Vector2 groundRayStart = Vector2.zero;
    private Vector2 centerRayStart = Vector2.zero;
    private Vector2 velo = Vector2.zero;
    private Rigidbody2D rb;
    private float lienencyTime = 0;
    private bool isGrounded = false;
    private RaycastHit2D ray;
    private string platformTag = "platform"; // using "platform" in update creates garbage
    private string horiz = "Horizontal";


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
        boxCol = GetComponent<BoxCollider2D>();

        groundRayStart.y -= boxCol.bounds.extents.y + 0.015f; //8
        groundRayStart.x = boxCol.bounds.extents.x + 0.00001f;
        centerRayStart.y = groundRayStart.y;

    }

    private void Update()
    {

        // check if grounded
        ray = Physics2D.Raycast(groundRayStart + (Vector2)transform.position, Vector2.down, groundCheckDistance);
        //Debug.DrawRay(groundRayStart + (Vector2)transform.position, Vector2.down * groundCheckDistance, Color.red);

        if (!ray || ray.transform.gameObject.tag != platformTag) // if ray not hitting, check other edge of player
        {
            groundRayStart.x *= -1;
            ray = Physics2D.Raycast(groundRayStart + (Vector2)transform.position, Vector2.down, groundCheckDistance);
            //Debug.DrawRay(groundRayStart + (Vector2)transform.position, Vector2.down * groundCheckDistance, Color.red);

            // if ray still not hitting, check directly below player. If not hit after this assume not on ground
            if (!ray || ray.transform.gameObject.tag != platformTag) 
            {
                ray = Physics2D.Raycast(centerRayStart + (Vector2)transform.position, Vector2.down, groundCheckDistance);
                //Debug.DrawRay(centerRayStart + (Vector2)transform.position, Vector2.down * groundCheckDistance, Color.red);

            }
        }

        if (ray && ray.transform.gameObject.tag == platformTag)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }


        // do extra grounded check to avoid false positives
        if (isGrounded && (rb.linearVelocityY > 0.001f || rb.linearVelocityY < -0.001f))
        {
            isGrounded = false;
        }

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

                if(velo.y > maxYVelo)
                {
                    velo.y = maxYVelo;
                    curPhase = JumpPhase.Deceleration;
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

        if (isGrounded)
        {
            //velo.y = 0;
        }
        else
        {
            velo.y += yIncrementalVelo * yVeloDirection * hoverMod * Time.deltaTime;
        }
        rb.linearVelocity = velo;
        //debugInfo();
    }

   
    private void OnTriggerEnter2D(Collider2D col)
    {
        if( col.gameObject.tag == platformTag && curPhase == JumpPhase.Acceleration)
        {
            
            curPhase = JumpPhase.Fall;
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
