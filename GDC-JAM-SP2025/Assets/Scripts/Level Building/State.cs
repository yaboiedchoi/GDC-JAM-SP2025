using UnityEngine;

public class State : MonoBehaviour
{
    private bool state;

    void Start() {
        state = false;
    }
    
    public bool IsOn() {
        return state;
    }
    public bool IsOff() {
        return !state;
    }

    public void SetStatus(bool new_status) {
        state = new_status;
    }
}
