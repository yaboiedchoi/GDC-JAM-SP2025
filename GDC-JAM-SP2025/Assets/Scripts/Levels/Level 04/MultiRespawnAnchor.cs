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

        // Check button states, in order of priority
        if (button2.state)
            newActiveIndex = 1;
        else if (button.state)
            newActiveIndex = 2;
        else if (button3.state)
            newActiveIndex = 3;

        if (newActiveIndex != -1 && newActiveIndex != lastActiveAnchorIndex)
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
            // No buttons are active — enable anchor 0
            foreach (var anchor in respawnAnchors)
            {
                anchor.TurnOff();
            }
            respawnAnchors[0].TurnOn();
            lastActiveAnchorIndex = 0;
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
