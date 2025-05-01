using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostIntroLevelManager : MonoBehaviour, LevelManager
{
    [SerializeField] int sanity = 4;
    [SerializeField] Button button1;
    [SerializeField] Door barrier1;
    [SerializeField] Button button2;
    [SerializeField] Door barrier2;
    [SerializeField] Button button3;
    [SerializeField] Door barrier3;
    [SerializeField] Button endButton;
    [SerializeField] Door endBarrier;


    private void Start()
    {
        setSanity();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            resetLevel();
        }

        barrier1.isOpen(button1.state);
        barrier2.isOpen(button2.state);
        barrier3.isOpen(button3.state);
        endBarrier.isOpen(endButton.state);

  


    }

    public void nextLevel()
    {
        SceneManager.LoadScene("Level 01");

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
