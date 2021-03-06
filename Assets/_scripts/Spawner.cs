﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] EnemyMovement objectToSpawn = null;
    [SerializeField] float secondsBetweenSpawns = 2f;
    [SerializeField] int spawnCount = 5;
    [SerializeField] AudioClip spawnSoundFX = null;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (spawnCount > 0)
        {
            GetComponent<AudioSource>().PlayOneShot(spawnSoundFX);
            var newEnemy = Instantiate(objectToSpawn, transform.position, Quaternion.identity);
            newEnemy.transform.parent = gameObject.transform;
            spawnCount--;
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }
    }
}
