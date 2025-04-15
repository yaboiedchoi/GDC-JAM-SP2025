using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    [SerializeField] float speed = 5;
    [SerializeField] float cooldownTime = 5;
    [SerializeField] float deviationTime = 5; // how long until deviation magnitude is re-randomized
    public bool goSpawn;

    static bool onCooldown = false; // shared between all ghosts

    GameObject player = null;
    Rigidbody2D rb;
    BoxCollider2D col;
    SpriteRenderer sr;
    float cooldownUntil; // time when cooldown is done
    float timeSinceDeviation = 0;
    Color color;
    Vector2 randomDeviation;
    float speedDeviation = 1;
    bool updatedCooldown = true;


    Vector2 playerDir;
    Vector3 spawnCoord;

  
    private void Awake()
    {
        spawnCoord = transform.position;

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
        color = sr.color;

        // random deviation vector defined once when ghost is initialized, then its magnitude is
        // randomly set every deviationTime seconds
        randomDeviation.x = UnityEngine.Random.Range(-1, 1);
        randomDeviation.y = UnityEngine.Random.Range(-1, 1);
        speedDeviation = UnityEngine.Random.Range(2f, 4);


        player = GameObject.Find("Player");
        if (player == null)
        {
            Debug.LogError("No player in scene");
        }
    }

    private void Update()
    {
        if(timeSinceDeviation > Time.time + deviationTime)
        {
            timeSinceDeviation = 0;

            randomDeviation.x = UnityEngine.Random.Range(-1, 1);
            randomDeviation.y = UnityEngine.Random.Range(-1, 1);


        }
        else
        {
            timeSinceDeviation += Time.deltaTime;
        }

        if(onCooldown || !updatedCooldown)
        {

            if (Time.time >= cooldownUntil)
            {
                onCooldown = false;
                col.enabled = true;
                sr.color = new Color(color.r, color.g, color.b, 1f);
                updatedCooldown = true;
            }

            playerDir = goSpawn ?  spawnCoord - transform.position : Vector2.zero;

        }
        else
        {

            playerDir = player.transform.position - transform.position;

        }

        playerDir += (playerDir.magnitude <= 1) ? Vector2.zero : randomDeviation * 0.33f * playerDir.magnitude;

        if (playerDir.magnitude <= 0.1f)
        {
            playerDir = Vector2.zero;
        }

        rb.linearVelocity = playerDir.normalized * speed * speedDeviation;

    }

    public void activateCooldown()
    {
        onCooldown = true;
        col.enabled = false;
        sr.color = new Color(color.r, color.g, color.b, 0.5f);
        cooldownUntil = Time.time + cooldownTime;
        rb.linearVelocity = Vector2.zero;
        updatedCooldown = false;
    }
}
