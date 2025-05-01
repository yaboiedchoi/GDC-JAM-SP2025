using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] AudioSource SFX;

    AudioClip footstepClip;
    [SerializeField] AudioClip backgroundClip;

    [NonSerialized] public float volume = 0.5f;

    AudioSource clone;

    bool playing;
    float pitch;
    private void Start()
    {
        pitch = SFX.pitch;
        playLooped(backgroundClip, 0.8f * volume);
    }

    public void setMusicVolume(float volume)
    {
        this.volume = volume;

        if (clone != null)
        {
            clone.volume = volume;
        }
    }
    private void playFootstep()
    {
        SFX.pitch = UnityEngine.Random.Range(0.6f, 1.4f) * pitch;
        playOnce(footstepClip, (SFX.volume * 0.5f) * volume );
    }

    public void startFootstep(float footstepSpeed, AudioClip footstep)
    {
        footstepClip = footstep;
        if(!playing)
        {
            InvokeRepeating(nameof(playFootstep), 0, footstepSpeed);
        }
        playing = true;
    }

    public void stopFootstep()
    {
        CancelInvoke(nameof(playFootstep));
        playing = false;
    }

    public void playOnce(AudioClip file, float vol)
    {
        AudioSource clone = gameObject.AddComponent<AudioSource>();
        clone.clip = file;
        clone.volume = vol * 2 * volume;
        clone.pitch = 1;
        clone.loop = false;
        clone.Play();
        // then delete it if I need?
    }

    public void playLooped(AudioClip file, float vol)
    {
        clone = gameObject.AddComponent<AudioSource>();
        clone.clip = file;
        clone.volume = volume;
        clone.pitch = 1;
        clone.loop = true;
        clone.Play();
        // cant delete if its looping.
    }

}
