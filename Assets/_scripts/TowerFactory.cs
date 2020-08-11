using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] int towerLimit = 4;
    [SerializeField] GameObject tower = null;
    [SerializeField] Transform towerParent = null;
    

    Queue<GameObject> towers = new Queue<GameObject>();

    public void AddTower(Waypoint baseWaypoint)
    {
        if (towers.Count < towerLimit)
        {
            InstantiateNewTower(baseWaypoint);
        }
        else
        {
            MoveExistingTower(baseWaypoint);
        }
        
    }

    private void InstantiateNewTower(Waypoint baseWaypoint)
    {
        baseWaypoint.isPlaceable = false;
        var newTower = Instantiate(tower, baseWaypoint.transform.position, Quaternion.identity);
        newTower.transform.parent = towerParent.transform;

        newTower.GetComponent<Tower>().waypoint = baseWaypoint;

        towers.Enqueue(newTower);
    }

    private void MoveExistingTower(Waypoint newBaseWaypoint)
    {
        var oldTower = towers.Dequeue();

        oldTower.GetComponent<Tower>().waypoint.isPlaceable = true;
        newBaseWaypoint.isPlaceable = false;

        oldTower.transform.position = newBaseWaypoint.transform.position;

        towers.Enqueue(oldTower);

    }
}
