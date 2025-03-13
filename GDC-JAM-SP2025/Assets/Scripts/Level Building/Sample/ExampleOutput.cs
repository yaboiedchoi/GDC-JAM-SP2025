using UnityEngine;

public class ExampleOutput : MonoBehaviour
{
    [SerializeField]
    private State state;

    private SpriteRenderer renderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state.IsOn()) {
            renderer.color = Color.green;
        } else {
            renderer.color = Color.red;
        }
    }
}
