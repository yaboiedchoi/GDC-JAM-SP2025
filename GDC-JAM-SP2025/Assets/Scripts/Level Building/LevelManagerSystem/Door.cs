using System;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    [NonSerialized] public bool isUnlocked = false;

    BoxCollider2D doorCol;
    SpriteRenderer doorRender;
    Color orig_color;


    private void Start()
    {
        doorCol = GetComponent<BoxCollider2D>();
        doorRender = GetComponent<SpriteRenderer>();
        orig_color = doorRender.color;
    }

    public void openDoor()
    {
        doorCol.enabled = false;
        doorRender.color = Color.gray;
    }

    public void closeDoor()
    {
        doorCol.enabled = true;
        doorRender.color = orig_color;
    }
}
