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
