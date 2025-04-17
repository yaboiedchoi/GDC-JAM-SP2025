using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

interface LevelManager
{
    public void nextLevel();
    public void setSanity();
    public void resetLevel();
}
public class DemoPlayerManager : MonoBehaviour, LevelManager
{
    [SerializeField] Lever lever;
    [SerializeField] Door exitDoor;
    [SerializeField] Door otherDoor;

    // respawn anchors
    [SerializeField] Button leftAnchorButton;
    [SerializeField] Button rightAnchorButton;
    [SerializeField] RespawnAnchor leftAnchor;
    [SerializeField] RespawnAnchor rightAnchor;

    private int sanity = 3;

    private void Start()
    {
        otherDoor.openDoor();
        setSanity();
    }
    private void Update()
    {
        exitDoor.isOpen(lever.signal);
        otherDoor.isOpen(!lever.signal);

        if (leftAnchorButton &&
            rightAnchorButton &&
            leftAnchor &&
            rightAnchor) {
            if (leftAnchorButton.state)
                RespawnManager.Instance.respawnAnchors.Add(leftAnchor);
            else if(!leftAnchorButton.state) 
                RespawnManager.Instance.respawnAnchors.Remove(leftAnchor);
            else if(rightAnchorButton.state) 
                RespawnManager.Instance.respawnAnchors.Add(rightAnchor);
            else if(!rightAnchorButton.state) 
                RespawnManager.Instance.respawnAnchors.Remove(rightAnchor);
        }

        for (int i = 0; i < RespawnManager.Instance.respawnAnchors.Count; i++)
        {
            if (i == 0) {
                RespawnManager.Instance.respawnAnchors[i].TurnOn();
            }
            else {
                RespawnManager.Instance.respawnAnchors[i].TurnOff();
            }
        }

        // Allow to reset level by pressing r
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
