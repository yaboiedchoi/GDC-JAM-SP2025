using UnityEngine;
using UnityEngine.SceneManagement;

public class CorpseButtonIntro : MonoBehaviour, LevelManager
{
    [SerializeField] int sanity = 1;

    [SerializeField] Button button;
    [SerializeField] Door door;

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

        if (button.state)
            door.openDoor();
        else if (!button.state)
            door.closeDoor();
    }
    public void nextLevel()
    {
        SceneManager.LoadScene("Level 02");
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
