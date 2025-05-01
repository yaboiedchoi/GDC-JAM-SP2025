using UnityEngine;
using UnityEngine.UI;

public class pauseManager : MonoBehaviour
{
    public static float volume = 0.5f;
    [SerializeField] Canvas pauseUI;
    [SerializeField] Slider slide;

    GameObject audioObject;
    AudioManager audMan;
    AudioSource audio;

    bool isActive = false;

    private void Awake()
    {
        audioObject = GameObject.FindGameObjectWithTag("audio");
        audio = audioObject.GetComponent<AudioSource>();
        audMan = audioObject.GetComponent<AudioManager>();
        slide.value = volume;
        audio.volume = volume;
        pauseUI.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isActive= !isActive;
            pauseUI.gameObject.SetActive(isActive);
            Time.timeScale = !isActive ? 1.0f : 0.0f;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void setVolume()
    {
        volume = slide.value;
        audio.volume = volume;
        audMan.setMusicVolume(volume);
        MusicManage.Instance.adjustBackVolume(volume);
    }

}
