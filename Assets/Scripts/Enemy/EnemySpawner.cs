using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;

    public Vector3 spawnLoc;

    void Start()
    {
        StartCoroutine(SpawnTop());
        StartCoroutine(SpawnBot());
        StartCoroutine(SpawnLeft());
        StartCoroutine(SpawnRight());
    }

    public IEnumerator SpawnTop()
    {
        while (true) 
        {
            yield return new WaitForSeconds(Random.Range(9f, 14f));
            spawnLoc = new Vector3 (Random.Range(-10f, 10f), 6f);
            Instantiate(enemy, spawnLoc, Quaternion.identity);
        }
    }

    public IEnumerator SpawnBot()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(9f, 14f));
            spawnLoc = new Vector3(Random.Range(-10f, 10f), -6f);
            Instantiate(enemy, spawnLoc, Quaternion.identity);
        }
    }

    public IEnumerator SpawnLeft()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(9f, 14f));
            spawnLoc = new Vector3(-10, Random.Range(-6f, 6f));
            Instantiate(enemy, spawnLoc, Quaternion.identity);
        }
    }

    public IEnumerator SpawnRight()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(9f, 14f));
            spawnLoc = new Vector3(10, Random.Range(-6f, 6f));
            Instantiate(enemy, spawnLoc, Quaternion.identity);
        }
    }
}
