using UnityEngine;
using UnityEngine.UI;

public class pauseManager : MonoBehaviour
{
    public static float volume = 0.5f;
    GameObject pauseUI;
    static Slider slide;

    GameObject audioObject;
    static AudioManager audMan;
    static AudioSource audio;

    bool isActive = false;

    private void Awake()
    {
        pauseUI = GameObject.FindGameObjectWithTag("pause");
        slide = pauseUI.GetComponentInChildren<Slider>();
        audioObject = GameObject.FindGameObjectWithTag("audio");
        audio = audioObject.GetComponent<AudioSource>();
        audMan = audioObject.GetComponent<AudioManager>();

        slide.value = volume;
        audio.volume = volume;
        pauseUI.gameObject.SetActive(false);

        Debug.Log(pauseUI.name + ", " + slide.name + ", " + audioObject.name + ", " + audio.name + ", " + audMan.name);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isActive= !isActive;
            pauseUI.SetActive(isActive);
            Time.timeScale = !isActive ? 1.0f : 0.0f;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public static void setVolume()
    {
        if (slide == null)
            Debug.LogError("bad");
        volume = slide.value;
        audio.volume = volume;
        audMan.setMusicVolume(volume);
        MusicManage.Instance.adjustBackVolume(volume);
    }

}
