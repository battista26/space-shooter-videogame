using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    // ID for powerups
    // 0 = tripleshot
    // 1 = speed
    // 2 = shields
    [SerializeField]
    private int powerupID;
    [SerializeField]
    private AudioSource powerupAudio;

    // Start is called before the first frame update
    void Start()
    {
        powerupAudio = GameObject.Find("PowerupAudio").GetComponent<AudioSource>();

        if (powerupAudio != null )
        {
            Debug.Log("Powerup Audio is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // move down at speed of 3 (adjust in inspector)
        // when leave screen destroy this object

        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -5.75)
        {
            Destroy(this.gameObject);
        }
        
    }

    // OnTriggerCollission
    // Only collectable by the Player (HINT: Use tags)
    // on collection destroy

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // communicate with player script (other)
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                switch (powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        powerupAudio.Play();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        powerupAudio.Play();
                        break;
                    case 2:
                        player.ShieldActive();
                        powerupAudio.Play();
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
