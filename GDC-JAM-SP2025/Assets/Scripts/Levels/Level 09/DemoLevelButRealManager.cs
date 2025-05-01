using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoLevelButRealManager : MonoBehaviour, LevelManager
{
    [SerializeField] Lever lever;
    [SerializeField] Door exitDoor;
    [SerializeField] Door otherDoor;

    [SerializeField] private int sanity = 3;

    private void Start()
    {
        setSanity();
    }
    private void Update()
    {
        exitDoor.isOpen(lever.signal);
        otherDoor.isOpen(!lever.signal);

        // Allow to reset level by pressing r
        if (Input.GetKeyDown(KeyCode.R))
        {
            resetLevel();
        }
    }

    public void nextLevel()
    {
        SceneManager.LoadScene("End Menu");
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
