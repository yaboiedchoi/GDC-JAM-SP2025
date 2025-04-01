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

    private void Start()
    {
        movementScript = GetComponent<PlayerMovement>();
        curSanity = maxSanity;
    }

    /*
     * Functionality for what happens when player dies. In future could put animation and/or particle system stuff in here.
     * Also any logic for sanity and stuff too
     * 
     * For now, it just "respawns" (teleports) the player to the spawn point
     */
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
}
