using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;

    PlayerMovement movementScript;
    private void Start()
    {
     movementScript = GetComponent<PlayerMovement>();   
    }

    /*
     * Functionality for what happens when player dies. In future could put animation and/or particle system stuff in here.
     * Also any logic for sanity and stuff too
     * 
     * For now, it just "respawns" (teleports) the player to the spawn point
     */
    public void killPlayer()
    {
        sendToSpawn();
        movementScript.resetMovement();

    }

    // Putting player spawning in seperate public function in case other scripts want to be able to spawn player outside of player dying
    public void sendToSpawn() 
    { 
        transform.position = spawnPoint.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "hazard")
        {
            killPlayer();
        }
    }
}
