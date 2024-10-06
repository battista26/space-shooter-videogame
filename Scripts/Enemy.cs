using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    private Player _player;
    // handle to animator component
    private Animator _animation;
    [SerializeField]
    private AudioSource _explosionAudio;
    [SerializeField]
    private GameObject _enemyLaser;
    public int cooldown;

    // Start is called before the first frame update
    void Start()
    {
        cooldown = Random.Range(3, 8);
        StartCoroutine(EnemyLaser());
        

        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.Log("Player is null");
        }

        _explosionAudio = GameObject.Find("Explosion").GetComponent<AudioSource>();
        if (_explosionAudio == null)
        {
            Debug.Log("Audio is null");
        }
        // null check player
        // assign the component
        _animation = GetComponent<Animator>();
        if (_animation == null)
        {
            Debug.Log("Animator is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5.4f)
        {
            transform.position = new Vector3(Random.Range(-9.5f, 9.5f), 6.2f, 0);
        }

        EnemyLaser();
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log("Hit:" + other.transform.name);

        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }
            // trigger the animation
            _animation.SetTrigger("OnEnemyDeath");
            // clip here
            _explosionAudio.Play();
            _speed = 0;

            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.5f);
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            // add 10 to score
            if (_player != null)
            {
                _player.AddScore(10);
            }
            // trigger the animation
            _animation.SetTrigger("OnEnemyDeath");
            // clip here
            _explosionAudio.Play();
            _speed = 0;

            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.5f);
        }
    }

    IEnumerator EnemyLaser()
    {
        while (true)
        {
            Instantiate(_enemyLaser, new Vector3(transform.position.x, transform.position.y + -1.5f, 0), Quaternion.identity);
            yield return new WaitForSeconds(cooldown);
        }
    }
}
