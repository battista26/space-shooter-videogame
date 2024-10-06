using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    // Start is called before the first frame update
    private float _speed = 6.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    // Variable reference to the shield visualizer
    [SerializeField]
    private GameObject _shieldVisual;
    [SerializeField]
    private GameObject _rightEngine;
    [SerializeField]
    private GameObject _leftEngine;
    [SerializeField]
    private float _fireRate = 0.15f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _isTripleShotActive = false;
    private bool _isShieldActive = false;

    private int _score;
    private UIManager _uiManager;

    [SerializeField]
    private int PlayerID;
    private string sceneName;

    // variable to store audio clip
    [SerializeField]
    private AudioSource _laserShotAudio;
    [SerializeField]
    private AudioSource _explosionAudio;
    [SerializeField]
    private GameObject _explosionPrefab;
    private GameObject clone;

    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene != null)
        {
            sceneName = currentScene.name;
        }

        if (sceneName == "Game")
        {
            if (PlayerID == 0)
            {
                transform.position = new Vector3(0, 0, 0);
            }
        }
        else if (sceneName == "Co-op")
        {
            if (PlayerID == 0)
            {
                transform.position = new Vector3(-5, 0, 0);
            }
            if (PlayerID == 1)
            {
                transform.position = new Vector3(5, 0, 0);
            }
        }

        
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is null");
        }

        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is null");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerID == 0)
        {
            CalculateMovement();
            FireLaser();
        }
        
        if (PlayerID == 1)
        {
            MovementPlayer2();
            FireLaserPlayer2();
        }
    }

    void CalculateMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
        }

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time > _canFire)
        {
            _canFire = Time.time + _fireRate;
            
            
            // if space key pressed,
            // if isTripleShotActive true
                // fire 3 lasers
            // else
                // fire 1 laser

            if (_isTripleShotActive == true)
            {
                Instantiate(_tripleShotPrefab, transform.position + new Vector3(0.22f, -0.13f, 0), Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, new Vector3(transform.position.x, transform.position.y + 1.01f, 0), Quaternion.identity);
            }
            
            // instantiate 3 lasers (triple shot prefab)

            // play the audio clip
            _laserShotAudio.Play();
        }
    }

    void MovementPlayer2()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
        }


        // character cannot go above -3.8f
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);


        // screen left and right
        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void FireLaserPlayer2()
    {
        if (Input.GetKey(KeyCode.Return) && Time.time > _canFire)
        {
            _canFire = Time.time + _fireRate;


            // if space key pressed,
            // if isTripleShotActive true
            // fire 3 lasers
            // else
            // fire 1 laser

            if (_isTripleShotActive == true)
            {
                Instantiate(_tripleShotPrefab, transform.position + new Vector3(0.22f, -0.13f, 0), Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, new Vector3(transform.position.x, transform.position.y + 1.01f, 0), Quaternion.identity);
            }

            // instantiate 3 lasers (triple shot prefab)

            // play the audio clip
            _laserShotAudio.Play();
        }
    }

    public void Damage()
    {
        // if shield is active
        // do nothing
        // deactivate shield
        // return;

        if (_isShieldActive == true)
        {
            _isShieldActive = false;
            // disable the visualizer
            _shieldVisual.SetActive(false);
            return;
        }
        else
        {
            _lives--;

            // if lives is 2
            // enable right
            // else if lives is 1
            // enable left

            if (_lives == 2)
            {
                _rightEngine.SetActive(true);
            }
            else if (_lives == 1)
            {
                _leftEngine.SetActive(true);
            }

            _uiManager.UpdateLives(_lives);
            if (_lives == 0)
            {
                _uiManager.GameOverScreen();
            }
        }

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            // instantiate explosion
            clone = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _explosionAudio.Play();
            _uiManager.CheckForBestScore();
            Destroy(this.gameObject);
            Destroy(clone, 2.4f);
        }
    }

    public void TripleShotActive()
    {
        // triple shot becomes true
        // start the power down coroutine
        _isTripleShotActive = true;
        StartCoroutine(TripleShotRoutine());
    }

    public void SpeedBoostActive()
    {
        StartCoroutine(SpeedBoostRoutine());
    }

    public void ShieldActive()
    {
        _isShieldActive = true;
        // enable the visualizer
        _shieldVisual.SetActive(true);
    }

    // method to add 10 to score
    // communicate with the UI to update the score
    public void AddScore(int points)
    {
        _score = _score + points;
        _uiManager.UpdateScore(_score);
    }


    // IEnumerator
    // wait 5 seconds
    // set triple shot to false

    IEnumerator TripleShotRoutine()
    {
        yield return new WaitForSeconds(5);
        _isTripleShotActive = false;
    }

    IEnumerator SpeedBoostRoutine()
    {
        _speed = 10.0f;
        yield return new WaitForSeconds(5);
        _speed = 6.5f;
    }
}
