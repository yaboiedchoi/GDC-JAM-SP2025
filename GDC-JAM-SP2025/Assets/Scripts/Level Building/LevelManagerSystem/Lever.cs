using UnityEngine;

public class Lever : MonoBehaviour, IInteractable
{

    [SerializeField] GameObject UI;
    public bool signal = false;

    private void Start()
    {
        if (UI != null) 
            UI.SetActive(false);
    }
    public void interact()
    {
        signal = !signal;
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
