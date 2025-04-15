using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;


public class GhostSpawner : MonoBehaviour
{
    [SerializeField] GameObject ghost;
    static GameObject ghostPrefab = null;

    public static List<GameObject> ghostList = new List<GameObject>();

    static Quaternion rotation = Quaternion.identity;

    private void Start()
    {
        if (ghostPrefab == null)
        {
            ghostPrefab = ghost;
        }
    }

    public static void spawnGhost(Vector3 spawnLocation)
    {
        Debug.Log("Spawned ghost");
        ghostList.Add(Instantiate(ghostPrefab, spawnLocation, rotation));
    }

    public static void killGhost()
    {
        // Hopefully ghost is added to list in same order as the corpse it spawned from is added to corpse list
        // It should I think
        Destroy(ghostList[0]);
        ghostList.RemoveAt(0);
    }
}
