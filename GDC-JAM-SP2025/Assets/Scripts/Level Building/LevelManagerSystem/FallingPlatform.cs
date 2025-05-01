using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] float holdTime; // Time before platform falls after player collided
    [SerializeField] float despawnTime; // Set to 0 or less to have platform never despawn

    Rigidbody2D rb;

    string playerTag = "Player";

    private AudioManager audioManager;
    [SerializeField] AudioClip clunk;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == playerTag)
        {
            StartCoroutine(DropPlatform());
        } else
        {
            audioManager.playOnce(clunk, 0.4f);
        }
    }

    IEnumerator DropPlatform()
    {
        yield return new WaitForSeconds(holdTime);

        // start falling
        rb.bodyType = RigidbodyType2D.Dynamic;

        if(despawnTime > 0)
        {
            yield return new WaitForSeconds(despawnTime);
            Destroy(gameObject);
        }


    }
}
