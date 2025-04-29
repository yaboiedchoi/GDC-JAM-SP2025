using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager Instance;

    public List<RespawnAnchor> respawnAnchors;

    [SerializeField]
    private GameObject respawnPoint;
    private void Awake() 
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        respawnAnchors = new List<RespawnAnchor>();
    }

    // Update is called once per frame
    void Update()
    {
        // get new list of anchors
        // set new spawn point
        if (respawnAnchors.Count > 0) {
            respawnPoint.transform.position = respawnAnchors[0].transform.position;
        }
    }
}
