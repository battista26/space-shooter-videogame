using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private GameObject _explosionPrefab;
    private GameObject clone;
    [SerializeField]
    private float _rotateSpeed = 20.0f;
    private SpawnManager _spawnManager;
    [SerializeField]
    private AudioSource _explosionAudio;

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }

    // check laser collison
    // instantiate explosion at the position of the astreoid
    // destroy explosion after 3 seconds

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            clone = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _explosionAudio.Play();
            Destroy(other.gameObject);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject, 0.15f);
            Destroy(clone, 2.4f);
        }
    }
}
