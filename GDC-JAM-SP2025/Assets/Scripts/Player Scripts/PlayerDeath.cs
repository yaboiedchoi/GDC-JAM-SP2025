using System;
using TMPro;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject deadBodyPrefab;
    [SerializeField] TMP_Text sanityUi;

    [NonSerialized] public int curSanity = 0;
    public int maxSanity = 5;


    Vector3 lastPos;
    Quaternion lastRot;
    float deathCooldown = 0.1f; // time determining when player can die again. Needed to prevent a single death counting as 2
    float lastDeathTime;

    PlayerMovement movementScript;

    GhostMovement[] ghosts;

    private void Start()
    {
        movementScript = GetComponent<PlayerMovement>();
        curSanity = maxSanity;
    }

    public void killPlayer()
    {
        lastPos = transform.position;
        lastRot = transform.rotation;
        sendToSpawn();
        movementScript.resetMovement();
        curSanity--;
        updateSanityUI();

        Instantiate(deadBodyPrefab, lastPos, lastRot);

        lastDeathTime = Time.time;

        // set cooldown to all ghosts
        ghosts = FindObjectsByType<GhostMovement>(FindObjectsSortMode.None); 
        // this might be not very efficient, but killPlayer() should happen sparce enough that it doesnt really matter.
        // can change later to a data struct that a ghost is added/removed to dynamically through the enemy spawner.
        // or even better something could be done with a static var
        foreach (GhostMovement ghost in ghosts)
        {
            ghost.activateCooldown();
        }

    }

    // Putting player spawning in seperate public function in case other scripts want to be able to spawn player outside of player dying
    public void sendToSpawn() 
    { 
        transform.position = spawnPoint.position;
    }

    public void updateSanityUI()
    {
        sanityUi.text = "Sanity: " + curSanity + "/" + maxSanity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "hazard" && Time.time > lastDeathTime + deathCooldown)
        {
            killPlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "hazard" && Time.time > lastDeathTime + deathCooldown)
        {
            killPlayer();
        }
    }
}
