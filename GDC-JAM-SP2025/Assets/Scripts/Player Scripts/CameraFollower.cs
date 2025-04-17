using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField]
    GameObject target;

    [SerializeField]
    float zoom = 5;

    Camera this_camera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this_camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, -1);

        this_camera.orthographicSize = zoom;
    }
}
