using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] float holdTime; // Time before platform falls after player collided

    Rigidbody2D rb;

    string playerTag = "Player";

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == playerTag)
        {
            StartCoroutine(DropPlatform());
        }
    }

    IEnumerator DropPlatform()
    {
        yield return new WaitForSeconds(holdTime);

        // start falling
        rb.bodyType = RigidbodyType2D.Dynamic;

        yield return new WaitForSeconds(holdTime);
        Destroy(gameObject); // probably bad idea but idc

    }
}
