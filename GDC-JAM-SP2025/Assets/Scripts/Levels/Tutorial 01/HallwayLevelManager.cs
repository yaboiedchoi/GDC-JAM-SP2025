using UnityEngine;
using UnityEngine.SceneManagement;

public class HallwayLevelManager : MonoBehaviour, LevelManager
{
    [SerializeField] int sanity = 7;


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
