using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public void LoadLevel()
    {
        SceneManager.LoadScene("Tutorial 01");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
