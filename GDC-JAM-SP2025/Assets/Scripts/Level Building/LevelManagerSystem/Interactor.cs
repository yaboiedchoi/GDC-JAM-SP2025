using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

interface IInteractable
{
    public void interact();
    public void interactUI(bool state);
}

public class Interactor : MonoBehaviour
{

    //[SerializeField] GameObject interactUI;
    [SerializeField] BoxCollider2D pCol;

    bool inRange = false;
    IInteractable interactable;

    void Start()
    {
        //interactUI.gameObject.SetActive(false);
    }

    // maybe issue when player is colliding with two triggers at the same time?
    /*private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out IInteractable interactObj))
        {
            // is an interactable object
            if (Input.GetKeyDown(KeyCode.E))
            {
                interactObj.interact();
            }
        }
    }*/


    // new better(?) way to chck for interactions

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out IInteractable interactObj))
        {
            interactable = interactObj;
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IInteractable interactObj) && interactable.Equals(interactObj))
        {
            inRange = false;
        }
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && inRange)
        {
            // player trying to interact, see if there is an interactable object
            interactable.interact();
        }
    }
}
