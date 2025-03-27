using UnityEditor.SearchService;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] GameObject levelManager;

    private void OnCollisionEnter2D(Collision2D collision)
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
