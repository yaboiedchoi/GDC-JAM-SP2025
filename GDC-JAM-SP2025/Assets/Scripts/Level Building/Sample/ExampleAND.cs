using UnityEngine;

public class ExampleAND : MonoBehaviour
{
    [SerializeField]
    State input_1;

    [SerializeField]
    State input_2;

    [SerializeField]
    State output;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        output.SetStatus(input_1.IsOn() && input_2.IsOn());
    }
}
