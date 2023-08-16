using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject[] enemiesPrefabs;

    [SerializeField] float topBound;
    [SerializeField] float downBound;
    [SerializeField] float leftBound;
    [SerializeField] float rightBound;

    [SerializeField] float startDelay = 2;
    [SerializeField] float spawnInterval = 1.5f;

    private void Start()
    {
        InvokeRepeating("SpawnRandomEnemies", startDelay, spawnInterval);
    }
    void SpawnRandomEnemies()
    {
        Vector3 spawn = new Vector3(Random.Range(downBound, topBound), Random.Range(leftBound, rightBound), 0);

        SpawnInDirection(spawn);
    }

    void SpawnInDirection(Vector3 position)
    {
        int enemyIndex = Random.Range(0, enemiesPrefabs.Length);
        Instantiate(enemiesPrefabs[enemyIndex], position, Quaternion.identity); ;
    }
}
