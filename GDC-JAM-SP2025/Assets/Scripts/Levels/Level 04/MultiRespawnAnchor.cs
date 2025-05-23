using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiRespawnAnchor : MonoBehaviour, LevelManager
{
    [SerializeField] int sanity = 3;

    [SerializeField] Button button;
    [SerializeField] Button button1;
    [SerializeField] Button button2;
    [SerializeField] Button button3;
    [SerializeField] RespawnAnchor respawn;
    [SerializeField] RespawnAnchor respawn1;
    [SerializeField] RespawnAnchor respawn2;
    [SerializeField] RespawnAnchor respawn3;
    [SerializeField] Door door;

    private List<Button> buttons;
    private List<RespawnAnchor> respawnAnchors;
    private int lastActiveAnchorIndex = -1;

    int activeRespawn = -1;
    bool bPrior = false;
    bool b2Prior = false;
    bool b3Prior = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        setSanity();
        buttons = new List<Button>();
        buttons.Add(button);
        buttons.Add(button1);
        buttons.Add(button2);
        buttons.Add(button3);
        respawnAnchors = new List<RespawnAnchor>();
        respawnAnchors.Add(respawn);
        respawnAnchors.Add(respawn1);
        respawnAnchors.Add(respawn2);
        respawnAnchors.Add(respawn3);
    }

    // Update is called once per frame
    // Add a variable to track the last active anchor index
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            resetLevel();
        }

        // Door logic (optional)
        if (button1.state)
            door.openDoor();
        else
            door.closeDoor();

        int newActiveIndex = -1;

        bool debugCheck = false;

        // In theory only one of these ifs should happen
        if (button.state && !bPrior)
        {
            // newly turned on, this is the active one
            activeRespawn = 0;
            bPrior = true;
            debugCheck = true;
            Debug.Log("0 Pressed");
        }
        if (button2.state && !b2Prior)
        {
            // just in case
            if (debugCheck)
                Debug.LogError("Something fucked up happened with respawn anchors");

            // newly turned on, this is the active one
            activeRespawn = 2;
            b2Prior = true;
            debugCheck = true;
            Debug.Log("2 Pressed");

        }
        if (button3.state && !b3Prior)
        {
            // just in case
            if (debugCheck)
                Debug.LogError("Something fucked up happened with respawn anchors");

            // newly turned on, this is the active one
            activeRespawn = 3;
            b3Prior = true;
            Debug.Log("3 Pressed");

        }

        // Set prior trackers to current state
        bPrior = button.state;
        b2Prior = button2.state;
        b3Prior = button3.state;


        switch (activeRespawn)
        {
            case 0:
                respawn2.TurnOn();
                respawn.TurnOff();
                respawn1.TurnOff();
                respawn3.TurnOff();
                break;
            case 2:
                respawn2.TurnOff();
                respawn.TurnOff();
                respawn1.TurnOn();
                respawn3.TurnOff();
                break;
            case 3:
                respawn2.TurnOff();
                respawn.TurnOff();
                respawn1.TurnOff();
                respawn3.TurnOn();
                break;

        }


        /*if (newActiveIndex != -1 && newActiveIndex != lastActiveAnchorIndex)
        {
            // Disable all anchors first
            foreach (var anchor in respawnAnchors)
            {
                anchor.TurnOff();
            }

            // Enable the new anchor
            respawnAnchors[newActiveIndex].TurnOn();
            lastActiveAnchorIndex = newActiveIndex;
        }
        else if (!button.state && !button2.state && !button3.state)
        {
            // No buttons are active � enable anchor 0
            foreach (var anchor in respawnAnchors)
            {
                anchor.TurnOff();
            }
            respawnAnchors[0].TurnOn();
            lastActiveAnchorIndex = 0;
        }*/
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
        SceneManager.LoadScene("Level 05");

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
