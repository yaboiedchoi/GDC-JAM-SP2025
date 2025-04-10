using System;
using UnityEngine;

public class RespawnAnchor : MonoBehaviour
{
    private bool isActive;

    [SerializeField] SpriteRenderer sr;

    // public property for triangle color 
    // neon blue for inactive, orange for active
    // blue RGB 0, 226, 255
    // orange RGB 255, 179, 0
    public Color TriangleColor {
        get {
            return sr.color;
        }
        set {
            sr.color = value;
        }
    }

    public bool IsActive {
        get {
            return isActive;
        }
        set {
            isActive = value;
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // get color value of the triangle on top of the anchor
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TurnOn() {
        isActive = true;
        TriangleColor = new Color(255, 179, 0);
    }
    public void TurnOff() {
        isActive = false;
        TriangleColor = new Color(0, 226, 255);
    }
}
