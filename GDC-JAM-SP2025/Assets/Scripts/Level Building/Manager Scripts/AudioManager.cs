using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] AudioSource SFX;

    AudioClip footstepClip;

    bool playing;
    float pitch;
    private void Start()
    {
        pitch = SFX.pitch;
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
}
