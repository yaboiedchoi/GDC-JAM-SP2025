using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager Instance;

    public Stack<GameObject> respawnAnchors;

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
        respawnAnchors = new Stack<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (respawnAnchors.Count > 0) {
            respawnPoint.transform.position = respawnAnchors.Peek().transform.position;
        }
    }
}
