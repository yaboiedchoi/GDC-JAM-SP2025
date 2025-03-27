using UnityEngine;

public class DemoPlayerManager : MonoBehaviour
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
}
