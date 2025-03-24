using UnityEngine;

public class DemoPlayerManager : MonoBehaviour
{
    [SerializeField] Lever lever;
    [SerializeField] Door door;

    private void Update()
    {
        if (lever.signal)
        {
            door.openDoor();
        }
        else
        {
            door.closeDoor();
        }
    }
}
