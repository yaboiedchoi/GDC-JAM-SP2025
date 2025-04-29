using UnityEditor.SearchService;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] GameObject levelManager;

    private void Start()
    {
        if (levelManager == null)
            levelManager = GameObject.FindGameObjectWithTag("Level Manager");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(levelManager != null && levelManager.TryGetComponent(out LevelManager levMan))
            {
                levMan.nextLevel();
            }
            else
            {
                Debug.LogError("No proper level manager attached to goal");
            }

        }
    }
}
