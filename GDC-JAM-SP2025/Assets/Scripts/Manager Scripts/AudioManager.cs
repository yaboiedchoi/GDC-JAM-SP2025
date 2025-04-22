using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] AudioSource SFX;

    bool firstPlay;
    float pitch;
    private void Start()
    {
        pitch = SFX.pitch;
    }
    public void playSoundEffect(AudioClip clip, bool randomizePitch)
    {
        float randomPitch = randomizePitch ? (UnityEngine.Random.Range(0.7f, 1.3f)) : 1f;
        SFX.pitch = pitch * randomPitch;

        if (SFX.clip != clip)
        {
            firstPlay = true;
            SFX.clip = clip;
            SFX.loop = true;
            SFX.time = 0;
            SFX.Play();
        }
        else
        {
            SFX.loop = true;
            if(!SFX.isPlaying)
            {
                SFX.Play();
            }
        }
    }

    public void stopLooping(AudioClip clip)
    {
        if (SFX.clip == clip)
        {
            SFX.loop = false;
        }
    }

}
