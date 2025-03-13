using UnityEngine;

public class ExampleInput : MonoBehaviour
{
    [SerializeField]
    private State state;

    [SerializeField]
    private KeyCode code;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(code)) {
            state.SetStatus(true);
        }
        else {
            state.SetStatus(false);
        }
    }
}
