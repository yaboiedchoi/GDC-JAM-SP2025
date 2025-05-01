using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyDyingLevelManager : MonoBehaviour, LevelManager
{
    [SerializeField] int sanity = 5;

    [SerializeField] Door horizonatalBarrier;
    [SerializeField] Door horizonatalBarrier2;
    [SerializeField] Door barrier;
    [SerializeField] Door mechanicalDoor;
    [SerializeField] Door endBarrier;
    [SerializeField] Door lateBarrier;


    [SerializeField] Lever barrierLever;
    [SerializeField] Lever endLever;
    [SerializeField] Lever endLever2;

    [SerializeField] RespawnAnchor startSpawn;
    [SerializeField] RespawnAnchor otherSpawn;
    [SerializeField] RespawnAnchor otherSpawn2;
    [SerializeField] RespawnAnchor endSpawn;

    bool state;
    bool lastState;
    int counter = 0;


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

        state = (barrierLever.signal);

        if (state != lastState)
        {
            counter++;
            if (counter > 3)
                counter = 0;
        }

        endBarrier.isOpen(endLever2.signal);
        horizonatalBarrier.isOpen(barrierLever.signal);
        horizonatalBarrier2.isOpen(!barrierLever.signal);
        mechanicalDoor.isOpen(barrierLever.signal ^ endLever.signal);
        barrier.isOpen(!endLever.signal ^ endLever2.signal);
        lateBarrier.isOpen(!endLever.signal ^ endLever2.signal);


        switch (counter)
        {
            case 0:
                startSpawn.TurnOn();
                otherSpawn.TurnOff();
                endSpawn.TurnOff();
                otherSpawn2.TurnOff();
                break;
            case 1:
                startSpawn.TurnOff();
                otherSpawn.TurnOn();
                endSpawn.TurnOff();
                otherSpawn2.TurnOff();
                break;
            case 2:
                startSpawn.TurnOff();
                otherSpawn.TurnOff();
                endSpawn.TurnOn();
                otherSpawn2.TurnOff();
                break;
            case 3:
                startSpawn.TurnOff();
                otherSpawn.TurnOff();
                endSpawn.TurnOff();
                otherSpawn2.TurnOn();
                break;
        }

        lastState = state;
    }


    public void nextLevel()
    {
        SceneManager.LoadScene("Level 08");

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
