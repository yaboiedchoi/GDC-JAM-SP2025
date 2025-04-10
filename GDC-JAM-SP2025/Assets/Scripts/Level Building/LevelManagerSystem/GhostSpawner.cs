using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    [SerializeField] static GameObject ghostPrefab;

    static Quaternion rotation = Quaternion.identity;
    public static void spawnGhost(Vector3 spawnLocation)
    {
        Instantiate(ghostPrefab, spawnLocation, rotation);
    }

    public static void killGhost()
    {

    }
}
