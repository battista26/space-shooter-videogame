using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    // handle to text
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartLevel;
    [SerializeField]
    private GameObject _pauseMenu;
    private GameManager _gameManager;
    [SerializeField]
    private Text bestScoreText;

    public int bestScore;
    private int currentScore;

    // Start is called before the first frame update
    void Start()
    {
        // assign text component to the handle
        _scoreText.text = "Score: " + 0;
        bestScore = PlayerPrefs.GetInt("HighScore", 0);
        bestScoreText.text = "Best: " + bestScore;
        _gameOverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if ( _gameManager == null )
        {
            Debug.LogError("Game Manager is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if p key is pressed pause the game
        if (Input.GetKeyDown(KeyCode.P))
        {
            _pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void UpdateScore(int playerscore)
    {
        currentScore = playerscore;
        _scoreText.text = "Score: " + playerscore;
    }

    // check for best score 
    // if bestscore 

    public void CheckForBestScore()
    {
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            PlayerPrefs.SetInt("HighScore", bestScore);
            bestScoreText.text = "Best: " + bestScore;
        }
    }

    public void UpdateLives(int currentLives)
    {
        // display img sprite
        // give it a new one based on the currentLives index
        _livesImg.sprite = _liveSprites[currentLives];
    }
    
    public void GameOverScreen()
    {
        _gameOverText.gameObject.SetActive(true);
        _restartLevel.gameObject.SetActive(true);
        _gameManager.GameOver();
        StartCoroutine(GameFlickerRoutine());
    }

    public void ResumeGame()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0); // 0 being the main menu
        Time.timeScale = 1.0f;
    }

    IEnumerator GameFlickerRoutine()
    {
        while (true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
