using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private void Start()
    {
        //Reset time scale
        Time.timeScale = 1f;
    }

    public void StartMultiplayer()
    {
        SceneManager.LoadScene("Pvp");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void DisableGameObject(GameObject gameObjectToDisable)
    {
        gameObjectToDisable.SetActive(false);
    }

    public void EnableGameObject(GameObject gameObjectToEnable)
    {
        gameObjectToEnable.SetActive(true);
    }
}
