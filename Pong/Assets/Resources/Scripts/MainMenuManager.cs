using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private Slider _masterVolumeSlider;

    private void Start()
    {
        //Reset time scale
        Time.timeScale = 1f;

        //Get master volume
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            AudioListener.volume = PlayerPrefs.GetFloat("MasterVolume");
            _masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        }
    }

    public void OnMasterVolumeChanged(System.Single NewVolume)
    {
        PlayerPrefs.SetFloat("MasterVolume", NewVolume);
        AudioListener.volume = PlayerPrefs.GetFloat("MasterVolume");
    }

    public void StartMultiplayer()
    {
        SceneManager.LoadScene("PvP");
    }

    public void StartSingleplayer()
    {
        SceneManager.LoadScene("PvE");
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
