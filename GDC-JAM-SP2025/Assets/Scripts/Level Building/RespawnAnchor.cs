using System;
using Unity.VisualScripting;
using UnityEngine;

public class RespawnAnchor : MonoBehaviour
{
    private bool isActive;

    [SerializeField] Sprite activeSprite;
    [SerializeField] Sprite deactiveSprite;

    [SerializeField] float hoverSpeed = 5f;
    [SerializeField] float switchTime = 0.5f;
    float startTime; // when hovering starts. randomized for variety
    bool hoverStarted = false;

    [SerializeField] bool startActive = false;

    [SerializeField] Rigidbody2D linkedObject; // will follow this object if not null

    float hoverDir = -1;
    Vector2 startPos; // need to stop slight drifting
    Vector2 startPos2;

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
        startPos = transform.position;
        startTime = UnityEngine.Random.Range(0, 0.5f);

        if (linkedObject != null )
            startPos2 = startPos - linkedObject.position;
        
        if (startActive)
        {
            TurnOn();
        }
    }

    private void FixedUpdate()
    {

        if (!hoverStarted && Time.time > startTime)
        {
            InvokeRepeating(nameof(switchDirection), 0, switchTime);
            hoverStarted = true;
        }

        if (linkedObject != null)
        {
            startPos = startPos2 + linkedObject.position;
            rb.linearVelocityY = hoverSpeed * hoverDir;
            rb.linearVelocityX = 0;
            rb.linearVelocity += linkedObject.linearVelocity;
        }

        if (transform.position.y < startPos.y)
        {
            transform.position = startPos;
        }
    }

    private void switchDirection()
    {
        hoverDir *= -1;
        rb.linearVelocityY = hoverSpeed * hoverDir;
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
