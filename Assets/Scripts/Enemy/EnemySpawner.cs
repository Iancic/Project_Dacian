using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;

    public Vector3 spawnLoc;

    public static int rounds = 0;

    void Start()
    {
        StartCoroutine(StartWAVE());
    }

    public IEnumerator StartWAVE()
    {
        while (true)
        {
            bool spawnDir = Random.value > 0.5;
            //returns true or false. 50/50 chance
            //THIS IS BASED ON DIFFICULTY TOO

            if (spawnDir)
            {
                //Top
                spawnLoc = new Vector3(Random.Range(-10f, 10f), 6f);
                Instantiate(enemy, spawnLoc, Quaternion.identity);
            }

            spawnDir = Random.value > 0.5;
            if (spawnDir)
            {
                //Bot
                spawnLoc = new Vector3(Random.Range(-10f, 10f), -6f);
                Instantiate(enemy, spawnLoc, Quaternion.identity);
            }

            spawnDir = Random.value > 0.5;
            if (spawnDir)
            {
                //Left
                spawnLoc = new Vector3(-10, Random.Range(-6f, 6f));
                Instantiate(enemy, spawnLoc, Quaternion.identity);
            }

            spawnDir = Random.value > 0.5;
            if (spawnDir)
            {
                //Right
                spawnLoc = new Vector3(10, Random.Range(-6f, 6f));
                Instantiate(enemy, spawnLoc, Quaternion.identity);
            }

            rounds++;

            yield return new WaitForSeconds(Random.Range(8f, 12f));
            //Based On Difficulty
        }
    }
}
