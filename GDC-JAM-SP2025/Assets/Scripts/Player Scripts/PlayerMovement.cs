using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Serialized Fields
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpPower = 5f;
    [SerializeField] float jumpMod = 1f; // modifier that allows to jump higher when holding jump, decays each frame
    [SerializeField] float jumpDecay = 2f; // determines rate of decay for jumpMod. jumpMod will be 0 in 1/jumpDecay seconds
    [SerializeField] float rapidJumpDecay = 4f; // determines rapid decay to yVelo when not holding space. yVelo will be 0 in 1/rapidFumpDecay seconds
    //[SerializeField] float freeAirTime = 0.1f; // seconds of time after releasing space that player hovers. 
    [SerializeField] float edgeJumpLienency = 1f;

    // Private Fields
    private Vector2 velo = Vector2.zero;
    private Rigidbody2D rb;
    private bool isGrounded = false;
    private float curJumpMod;
    private float maxYVelo = 0;
    private float freeAirUntil;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        // check for walking off platform without jumping
        // increasing threshold allows for more leniency with jumping while slightly off edge
        if (rb.linearVelocityY > edgeJumpLienency || rb.linearVelocityY < -edgeJumpLienency && isGrounded)
        {
            isGrounded = false;
        }

        // Detect jump input
        if (Input.GetKey(KeyCode.Space))
        {
            if(isGrounded)
            {
                isGrounded = false;
                curJumpMod = jumpMod;
                maxYVelo = jumpPower;
            }
            else
            {
                curJumpMod -= jumpDecay * jumpMod * Time.deltaTime;
                curJumpMod = (curJumpMod < 0) ? 0 : curJumpMod;
            }

            rb.linearVelocityY += curJumpMod * maxYVelo * Time.deltaTime;

            if(maxYVelo < rb.linearVelocityY)
            {
                maxYVelo = rb.linearVelocityY;
            }
        }
        else
        {
            // if let go of space, wont go higher
            curJumpMod = 0;

            // set y velo to slow down faster when not holding space
            // This is to make it easier and more responsive to stop gaining altitude
            if(velo.y > 0.01)
            {
                //two approaches to making jumps feel more responsive. Comment out whichever one feels worse

                rb.linearVelocityY -= (rapidJumpDecay * maxYVelo) * Time.deltaTime; 

                //freeAirUntil = Time.time + freeAirTime;
                //rb.linearVelocityY = 0;
            }
        }

        float xVelo = Input.GetAxisRaw("Horizontal");

        velo.x = xVelo * speed;

        if(Time.time > freeAirUntil)
        {
            velo.y = rb.linearVelocityY;
        }
        else
        {
            velo.y = 0;
        }

        //Debug.Log("Grounded: " + isGrounded + "\nVelo: " + velo);

        rb.linearVelocity = velo;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        // allow to jump again if touching platform while not having any Y velo (to prevent wall jumping)
        if(col.gameObject.CompareTag("platform") && rb.linearVelocityY == 0)
        {
            isGrounded = true;
        }
    }

}
