using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GatesIntro : MonoBehaviour, LevelManager
{
    [SerializeField] int sanity = 3;

    [SerializeField] Button button;
    [SerializeField] Button button1;
    [SerializeField] RespawnAnchor anchor;
    [SerializeField] RespawnAnchor anchor1;
    [SerializeField] RespawnAnchor anchor2;
    [SerializeField] Door door;

    private List<RespawnAnchor> respawnAnchors;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        setSanity();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            resetLevel();
        }

        if (button.state && button1.state)
        {
            door.openDoor();
        }
        else if (!button.state && !button1.state)
        {
            door.closeDoor();
        }
    }

    /// <summary>
    /// Turn off every respawn anchor except for the input index
    /// </summary>
    /// <param name="index">Input index of the respawn anchor that should stay on</param>
    private void enableAnchor(int index)
    {
        for (int i = 0; i < respawnAnchors.Count; i++)
        {
            if (i == index)
            {
                respawnAnchors[i].TurnOn();
                continue;
            }
            else
            {
                respawnAnchors[i].TurnOff();
            }
        }
    }
    public void nextLevel()
    {
        //SceneManager.LoadScene("Scene Name"); Do something like this to load next level
        Debug.Log("You beat the demo level!!!!!");
    }

    // Have to set static variable maxSanity, will likely just be called in start() or whatever you use for level setup
    public void setSanity()
    {
        PlayerDeath.setMaxSanity(sanity);
    }

    public void resetLevel()
    {
        /*
         * Using loadscene() to do this seems to take a little bit, might not really be a problem (especially if we a have a 
         * leading level screen we can put up, but if we want it to be faster, we could do something like
         * manually removing all corpses and ghosts, setting player to initial spawn, reset level elements, etc
         * 
         * but this is easier
         */
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }
}
