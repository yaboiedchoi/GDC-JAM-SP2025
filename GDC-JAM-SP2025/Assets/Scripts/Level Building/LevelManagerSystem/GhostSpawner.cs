using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;


public class GhostSpawner : MonoBehaviour
{
    [SerializeField] GameObject ghost;
    static GameObject ghostPrefab = null;


    [SerializeField] bool ghostGoSpawn;
    static bool goSpawn = false;

    public static List<GameObject> ghostList = new List<GameObject>();

    static Quaternion rotation = Quaternion.identity;

    private void Start()
    {
        if (ghostPrefab == null)
        {
            ghostPrefab = ghost;
            goSpawn = ghostGoSpawn;
        }
    }

    

    public static void spawnGhost(Vector3 spawnLocation)
    {
        GameObject ghost = Instantiate(ghostPrefab, spawnLocation, rotation);
        ghostList.Add(ghost);
        ghost.GetComponent<GhostMovement>().goSpawn = goSpawn;
    }

    public static void killGhost()
    {
        // Hopefully ghost is added to list in same order as the corpse it spawned from is added to corpse list
        // It should I think
        if (ghostList.Count > 0)
        {

            Destroy(ghostList[0]);
            ghostList.RemoveAt(0);
        }
    }
}
