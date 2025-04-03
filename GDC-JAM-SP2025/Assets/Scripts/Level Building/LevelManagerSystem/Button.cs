using System;
using UnityEngine;

public class Button : MonoBehaviour
{
    [NonSerialized] bool state = false;
    private string playerTag = "Player"; // using string literals in looping code creates garbage
    Color defaultColor;

    private void Start()
    {
        defaultColor = GetComponent<SpriteRenderer>().color;
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == playerTag && !state)
        {
            pressButton();
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == playerTag)
        {
            releaseButton();
        }
    }

    private void pressButton()
    {
        state = true;
        GetComponent<SpriteRenderer>().color = Color.cyan; // temp just to visually see button change until have actual sprite
    }

    private void releaseButton()
    {
        state = false;
        GetComponent<SpriteRenderer>().color = defaultColor; // also temp
    }
}
