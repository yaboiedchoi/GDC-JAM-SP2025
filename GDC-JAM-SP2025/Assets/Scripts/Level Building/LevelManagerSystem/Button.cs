using System;
using System.Collections;
using UnityEngine;

public class Button : MonoBehaviour
{

    [SerializeField] Sprite onSprite;
    Sprite offSprite;

    [NonSerialized] public bool state = false;
    private string playerTag = "Player"; // using string literals in looping code creates garbage

    private bool colliding = false;
    private void Start()
    {
        offSprite = GetComponent<SpriteRenderer>().sprite;
    }

    private void OnCollisionStay2D(Collision2D col) 
    {
        
        if (col.gameObject.tag == playerTag)
        {
            colliding = true;
            pressButton();
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == playerTag)
        {
            colliding = false;

            // causes annoying error when unloading scene, this is just to get rid of that
            if (gameObject.activeSelf)
                StartCoroutine(releaseButton());
        }
    }

    private void pressButton()
    {
        state = true;
        GetComponent<SpriteRenderer>().sprite = onSprite; // temp just to visually see button change until have actual sprite
    }

    IEnumerator releaseButton()
    {
        yield return new WaitForSeconds(0.1f); // need to pause for a tiny bit to ensure no false positive
        if (!colliding)
        {
            state = false;
            GetComponent<SpriteRenderer>().sprite = offSprite; // also temp
        }
    }
}
