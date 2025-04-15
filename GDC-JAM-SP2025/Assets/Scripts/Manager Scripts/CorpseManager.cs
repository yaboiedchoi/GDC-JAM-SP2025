using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CorpseManager : MonoBehaviour
{   
    // singleton
    public static CorpseManager Instance;

    // list of corpses
    public List<GameObject> corpses;

    // limited corpses
    public bool limitCorpses;

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
        corpses = new List<GameObject>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (limitCorpses && corpses.Count > 3) {
            GhostSpawner.killGhost();
            Destroy(corpses[0]);
            corpses.RemoveAt(0);
        }
    }
}
