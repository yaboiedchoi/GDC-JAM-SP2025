using UnityEngine;

public class MusicManage : MonoBehaviour
{
    private static MusicManage instance;
    private AudioSource audioSource;


    public static MusicManage Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MusicManage>();
            }
            return instance;
        }
    }


    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Kill duplicates
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("MusicManager active: " + gameObject.name);

        audioSource = GetComponent<AudioSource>();


    }

    public void adjustBackVolume(float vol)
    {
        if (audioSource != null)
        {
            audioSource.volume = vol;
        }
    }

}

