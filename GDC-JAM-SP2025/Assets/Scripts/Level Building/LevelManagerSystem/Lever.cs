using UnityEngine;

public class Lever : MonoBehaviour, IInteractable
{

    [SerializeField] GameObject UI;
    public bool signal = false;

    [SerializeField] SpriteRenderer sr;

    private void Start()
    {
        if (UI != null) 
            UI.SetActive(false);

        // get color of lever
        sr = GetComponent<SpriteRenderer>();
    }
    public void interact()
    {
        signal = !signal;
        // green if active, 107 255 0
        // purple if inactive, 150, 0, 255
        if (signal) {
            sr.flipX = true;
        }
        else {
            sr.flipX= false;
        }
    }

    public void interactUI(bool state)
    {
        if (UI != null) 
            UI.SetActive(state);
    }

    // may need better player detection in future
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (UI != null) 
            UI.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(UI != null)
            UI.SetActive(false);
    }
}
