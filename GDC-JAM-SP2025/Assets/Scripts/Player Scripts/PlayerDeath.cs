using System;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject deadBodyPrefab;
    [SerializeField] Slider sanityBar; 

    [NonSerialized] public static int curSanity = 0;
    private static int maxSanity = 5; // use setMaxSanity to set from another script. Otherwise this is default value


    Vector3 lastPos;
    Quaternion lastRot;
    float deathCooldown = 0.1f; // time determining when player can die again. Needed to prevent a single death counting as 2
    float lastDeathTime;

    PlayerMovement movementScript;

    static List<GhostMovement> ghosts = new List<GhostMovement>();

    private AudioManager audioManager;
    [SerializeField] AudioClip bleh;



    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
        movementScript = GetComponent<PlayerMovement>();
        curSanity = maxSanity;
    }

    // Sets max sanity and updates player to full sanity
    public static void setMaxSanity(int sanity)
    {
        maxSanity = sanity;
        curSanity = maxSanity;
    }

    // Getter for max sanity
    public static int getMaxSanity()
    {
        return maxSanity;
    }

  
    public void killPlayer()
    {
        audioManager.playOnce(bleh, 0.2f);
        lastPos = transform.position;
        lastRot = transform.rotation;
        curSanity--;

        if (curSanity <= 0)
        {
            GhostSpawner.spawnGhost(lastPos);
        }

        sendToSpawn();
        movementScript.resetMovement();
        updateSanityUI();
        if (CorpseManager.Instance)
            CorpseManager.Instance.corpses.Add(Instantiate(deadBodyPrefab, lastPos, lastRot));
        lastDeathTime = Time.time;

        foreach (GameObject ghost in GhostSpawner.ghostList)
        {
            if (ghost != null)
                ghost.GetComponent<GhostMovement>().activateCooldown();
        }

    }

    // Putting player spawning in seperate public function in case other scripts want to be able to spawn player outside of player dying
    public void sendToSpawn() 
    { 
        transform.position = spawnPoint.position;
    }

    public void updateSanityUI()
    {
        sanityBar.maxValue = maxSanity;
        sanityBar.value = curSanity;
        // also send a signal to ghostspawner
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
