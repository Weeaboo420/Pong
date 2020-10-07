using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    private int _leftPlayerScore = 0, _rightPlayerScore = 0;
    public Text PlayerOneScore, PlayerTwoScore;
    public GameObject BallPrefab, ExitScreen;
    public Slider MasterVolumeSlider;
    private bool DefaultMove = true;
    private AudioSource _audioSource;
    private float _currentMasterVolume;

    private void SpawnBall()
    {
        GameObject newBall = (GameObject)Instantiate(BallPrefab, new Vector3(0, Random.Range(-2.5f, 2.5f), 0), Quaternion.identity);

        Ball ballScript = newBall.GetComponent<Ball>();
        ballScript.MovingRight = DefaultMove;
        ballScript.MovingUp = DefaultMove;

        DefaultMove = !DefaultMove;

    }

    public void ToggleExitScreen()
    {
        ExitScreen.SetActive(!ExitScreen.activeSelf);

        if(!ExitScreen.activeSelf)
        {
            Time.timeScale = 1f;
        } else
        {
            Time.timeScale = 0f;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator StartNewRound()
    {
        yield return new WaitForSeconds(1.5f);
        SpawnBall();
    }

    private void ApplyMasterVolume()
    {
        AudioListener.volume = _currentMasterVolume;
    }

    public void OnMasterVolumeChanged(System.Single NewVolume)
    {
        PlayerPrefs.SetFloat("MasterVolume", NewVolume);
        _currentMasterVolume = NewVolume;
        ApplyMasterVolume();
    }

    private void UpdateUI()
    {
        PlayerOneScore.text = "Player 1: " + _leftPlayerScore.ToString();
        PlayerTwoScore.text = "Player 2: " + _rightPlayerScore.ToString();
    }

    private void Start()
    {
        UpdateUI();
        SpawnBall();
        _audioSource = GetComponent<AudioSource>();

        if(PlayerPrefs.HasKey("MasterVolume"))
        {
            _currentMasterVolume = PlayerPrefs.GetFloat("MasterVolume");
        } else
        {
            _currentMasterVolume = 1f;
        }

        MasterVolumeSlider.value = _currentMasterVolume;

        ApplyMasterVolume();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleExitScreen();
        }
    }

    public void AddScore(bool ToRightPlayer)
    {
        if(ToRightPlayer)
        {
            _rightPlayerScore++;
        } else
        {
            _leftPlayerScore++;
        }

        _audioSource.Play();

        UpdateUI();
        StartCoroutine(StartNewRound());
    }
}
