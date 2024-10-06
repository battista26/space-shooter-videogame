using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject[] powerups;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool _stopSpawning = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // spawn gameobject every 5 seconds
    // create a coroutine of IEnumerator -- yield events
    // while loop

    IEnumerator SpawnEnemyRoutine()
    {
        // while loop (infinite)
        // Instantiate enemy prefab
        // yield wait for 5 seconds

        yield return new WaitForSeconds(3.0f);

        while (_stopSpawning == false)
        {
            GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(Random.Range(-9.5f, 9.5f), 6.2f, 0), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5);
        }
        
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(1.0f);

        // every 3-7 seconds spawn in a powerup
        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(Random.Range(3, 8));
            int randomPowerup = Random.Range(0, 3);
            Instantiate(powerups[randomPowerup], new Vector3(Random.Range(-9.5f, 9.5f), 6.2f, 0), Quaternion.identity);
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
