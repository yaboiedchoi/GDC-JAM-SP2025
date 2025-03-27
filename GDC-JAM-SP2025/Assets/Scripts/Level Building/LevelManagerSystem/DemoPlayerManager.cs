using UnityEngine;

interface LevelManager
{
    public void nextLevel();
}
public class DemoPlayerManager : MonoBehaviour, LevelManager
{
    [SerializeField] Lever lever;
    [SerializeField] Door exitDoor;
    [SerializeField] Door otherDoor;

    private void Start()
    {
        otherDoor.openDoor();
    }
    private void Update()
    {
        exitDoor.isOpen(lever.signal);
        otherDoor.isOpen(!lever.signal);
    }

    public void nextLevel()
    {
        Debug.Log("You beat the demo level!!!!!");
    }
}
