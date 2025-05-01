using System.Threading.Tasks;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] GameObject levelManager;
    [SerializeField] AudioClip tp;

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
        if (levelManager == null)
            levelManager = GameObject.FindGameObjectWithTag("Level Manager");
    }


    private async Task OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(levelManager != null && levelManager.TryGetComponent(out LevelManager levMan))
            {
                audioManager.playOnce(tp, 0.4f);
                await Task.Delay(300);
                levMan.nextLevel();
            }
            else
            {
                Debug.LogError("No proper level manager attached to goal");
            }

        }
    }
}
