using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    //Parameters of each tower
    [SerializeField] Transform objectToPan = null;
    [SerializeField] float attackRange = 10f;
    [SerializeField] ParticleSystem bullet = null;

    public Waypoint waypoint;

    //State of each tower
    Transform targetEnemy = null;

    private void Update()
    {
        SetTargetEnemy();
        if (objectToPan != null && targetEnemy != null)
        {
            CheckForEnemy();
            objectToPan.LookAt(targetEnemy);
        }
        else
        {
            Shoot(false);
        }
    }

    private void SetTargetEnemy()
    {
        var enemies = FindObjectsOfType<EnemyMovement>();
        if(enemies.Length == 0) { return; }

        Transform closestEnemy = enemies[0].transform;

        foreach(EnemyMovement enemy in enemies)
        {
            closestEnemy = GetClosest(closestEnemy, enemy.transform);
        }
        targetEnemy = closestEnemy;
    }

    private Transform GetClosest(Transform transformA, Transform transformB)
    {
        var distanceToA = Vector3.Distance(transform.position, transformA.position);
        var distanceToB = Vector3.Distance(transform.position, transformB.position);

        if(distanceToA < distanceToB)
        {
            return transformA;
        }
        return transformB;
    }

    void CheckForEnemy()
    {
        float distanceToEnemy = Vector3.Distance(targetEnemy.transform.position, gameObject.transform.position);
        if(distanceToEnemy <= attackRange && targetEnemy != null)
        {
            Shoot(true);
        }
        else
        {
            Shoot(false);
        }
    }

    private void Shoot(bool isActive)
    {
        var emissionModule = bullet.emission;
        emissionModule.enabled = isActive;
    }
}
