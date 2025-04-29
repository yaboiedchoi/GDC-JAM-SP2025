using System;
using UnityEngine;

public class RespawnAnchor : MonoBehaviour
{
    private bool isActive;

    [SerializeField] Sprite activeSprite;
    [SerializeField] Sprite deactiveSprite;

    [SerializeField] float hoverSpeed = 5f;
    [SerializeField] float switchTime = 0.5f;

    [SerializeField] bool startActive = false;

    Rigidbody2D rb;
    SpriteRenderer sr;

    Transform spawnPoint;

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

        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocityY = hoverSpeed;
        spawnPoint = GameObject.FindGameObjectWithTag("spawn").transform;
        if (startActive)
        {
            TurnOn();
        }
        InvokeRepeating(nameof(switchDirection), 0, switchTime);
    }

    private void switchDirection()
    {
        rb.linearVelocityY *= -1;
    }

    public void TurnOn() {
        isActive = true;
        sr.sprite = activeSprite;
        spawnPoint.position = transform.position;
    }
    public void TurnOff() {
        isActive = false;
        sr.sprite = deactiveSprite;
    }
}
