using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

interface IInteractable
{
    public void interact(); // will have interactable do whatever its interaction is
    public void interactUI(bool state); // allow to manually activate any UI component of interactable
}


/*
 * The Interactor script should only go on the player, no other gameobject should have it. Instead, have whatever script attached to an
 * interactable object implement the IInteractable interface. Just code whatever effect you want in the interact() method. interactUI can
 * do nothing if you don't have UI associated with the object or don't want it to be set manually. Use a collider set to trigger to determine where
 * player can interact with your object.
 */
public class Interactor : MonoBehaviour
{

    bool inRange = false;
    IInteractable interactable;

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
            interactable.interact(); 
        }
    }
}
