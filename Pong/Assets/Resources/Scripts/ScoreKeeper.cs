using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreKeeper : MonoBehaviour
{        
    [SerializeField]
    private Text _playerOneScore, _playerTwoScore;

    [SerializeField]
    private GameObject _ballPrefab, _exitScreen;

    [SerializeField]
    private Slider _masterVolumeSlider;
    
    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private bool _isSinglePlayer = false;

    private float _currentMasterVolume;
    private bool _defaultMove = true;
    private int _leftPlayerScore = 0, _rightPlayerScore = 0;
    private GameObject _ball;
    private void SpawnBall()
    {
        GameObject newBall = (GameObject)Instantiate(_ballPrefab, new Vector3(0, Random.Range(-2.5f, 2.5f), 0), Quaternion.identity);

        Ball ballScript = newBall.GetComponent<Ball>();
        ballScript.MovingRight = _defaultMove;
        ballScript.MovingUp = _defaultMove;

        _ball = newBall;

        _defaultMove = !_defaultMove;

    }

    public void ToggleExitScreen()
    {
        _exitScreen.SetActive(!_exitScreen.activeSelf);

        if(!_exitScreen.activeSelf)
        {
            Time.timeScale = 1f;
            Cursor.visible = false;
        } else
        {
            Time.timeScale = 0f;
            Cursor.visible = true;
        }
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
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
        

        if (!_isSinglePlayer)
        {
            _playerOneScore.text = "Player 1: " + _leftPlayerScore.ToString();
            _playerTwoScore.text = "Player 2: " + _rightPlayerScore.ToString();
        } else
        {
            _playerOneScore.text = "Player: " + _leftPlayerScore.ToString();
            _playerTwoScore.text = "CPU: " + _rightPlayerScore.ToString();
        }
    }

    private void Start()
    {
        UpdateUI();
        SpawnBall();

        Cursor.visible = false;

        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            _currentMasterVolume = PlayerPrefs.GetFloat("MasterVolume");
        } else
        {
            _currentMasterVolume = 1f;
        }

        _masterVolumeSlider.value = _currentMasterVolume;

        ApplyMasterVolume();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) //Toggle the exit screen
        {
            ToggleExitScreen();
        }
        
        if (_ball != null) //Make sure we have a reference to the ball before looking at its position.
        {
            //If the ball travels outside the bounds, remove it, and spawn a new one.
            if (_ball.transform.position.x > Globals.BoundsX || _ball.transform.position.x < -Globals.BoundsX ||
                _ball.transform.position.y > Globals.BoundsY || _ball.transform.position.y < -Globals.BoundsY)
            {
                Destroy(_ball);
                StartCoroutine(StartNewRound());
            }
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
