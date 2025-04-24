using System;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{

    [SerializeField] float raiseSpeed = 5f;
    [SerializeField] float raiseMult = 1; // raises by height*raiseMult

    [NonSerialized] public bool isUnlocked = false;

    BoxCollider2D doorCol;
    SpriteRenderer doorRender;
    Rigidbody2D rb;
    Color orig_color;
    float raiseDist;
    bool pause;

    Vector2 ogPos;


    private void Start()
    {
        doorCol = GetComponent<BoxCollider2D>();
        doorRender = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        orig_color = doorRender.color;
        raiseDist = doorCol.bounds.extents.y * raiseMult * 2;
        ogPos = transform.position;
    }

    private void FixedUpdate()
    {
        if (isUnlocked && transform.position.y < raiseDist + ogPos.y && !pause)
        {
            // still needs to rise
            rb.linearVelocityY = raiseSpeed;
        }
        else if (!isUnlocked && transform.position.y > ogPos.y && !pause)
        {
            // still needs to fall
            rb.linearVelocityY = -raiseSpeed;
        }
        else
        {
            rb.linearVelocityY = 0;
        }
    }

    public void openDoor()
    {
        doorRender.color = Color.gray;
        isUnlocked = true;
    }

    public void closeDoor()
    {
        doorRender.color = orig_color;
        isUnlocked = false;
    }

    public void isOpen(bool state)
    {
        if(state)
        {
            openDoor();
        }
        else
        {
            closeDoor();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            pause = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            pause = false;
        }
    }
}
