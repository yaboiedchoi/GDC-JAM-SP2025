using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] AudioSource SFX;

    AudioClip footstepClip;
    [SerializeField] AudioClip backgroundClip;

    bool playing;
    float pitch;
    private void Start()
    {
        pitch = SFX.pitch;
        playLooped(SFX, backgroundClip);
    }
    private void playFootstep()
    {
        SFX.pitch = UnityEngine.Random.Range(0.6f, 1.4f) * pitch;
        SFX.PlayOneShot(footstepClip);
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

    public void playOnce(AudioSource og, AudioClip file)
    {
        AudioSource clone = gameObject.AddComponent<AudioSource>();
        clone.clip = file;
        clone.volume = og.volume;
        clone.pitch = 1;
        clone.loop = false;
        clone.Play();
        // then delete it if I need?
    }

    public void playLooped(AudioSource og, AudioClip file)
    {
        AudioSource clone = gameObject.AddComponent<AudioSource>();
        clone.clip = file;
        clone.volume = 0.4f;
        clone.pitch = 1;
        clone.loop = true;
        clone.Play();
        // cant delete if its looping.
    }

}
