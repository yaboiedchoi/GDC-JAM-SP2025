using UnityEngine;
using UnityEngine.SceneManagement;

interface LevelManager
{
    public void nextLevel();
    public void setSanity();
}
public class DemoPlayerManager : MonoBehaviour, LevelManager
{
    [SerializeField] Lever lever;
    [SerializeField] Door exitDoor;
    [SerializeField] Door otherDoor;

    // respawn anchors
    [SerializeField] Lever leftAnchorLever;
    [SerializeField] Lever rightAnchorLever;
    [SerializeField] RespawnAnchor leftAnchor;
    [SerializeField] RespawnAnchor rightAnchor;

    private int sanity = 5;

    private void Start()
    {
        otherDoor.openDoor();
        setSanity();
    }
    private void Update()
    {
        exitDoor.isOpen(lever.signal);
        otherDoor.isOpen(!lever.signal);
    }

    public void nextLevel()
    {
        //SceneManager.LoadScene("Scene Name"); Do something like this to load next level
        Debug.Log("You beat the demo level!!!!!");
    }

    // Have to set static variable maxSanity, will likely just be called in start() or whatever you use for level setup
    public void setSanity()
    {
        PlayerDeath.maxSanity = sanity;
    }
}
