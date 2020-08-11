using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] private Waypoint startPoint = null;
    [SerializeField] private Waypoint endPoint = null;

    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();

    bool isRunning = true;
    Waypoint searchCenter;

    public List<Waypoint> path = new List<Waypoint>();

    Vector2Int[] directions = {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    public List<Waypoint> GetPath()
    {
        if (path.Count == 0)
        {
            CalculatePath();
        }
        return path;
    }

    private void CalculatePath()
    {
        LoadBlocks();
        BreadthFirstSearch();
        CreatePath();
    }

    private void CreatePath() //Todo protect against no path
    {
        AddToPath(endPoint);

        Waypoint previous = endPoint.exploredFrom;

        while (previous != startPoint)
        {
            AddToPath(previous);
            previous = previous.exploredFrom;
        }
        AddToPath(startPoint);

        path.Reverse();
    }

    private void AddToPath(Waypoint waypoint)
    {
        path.Add(waypoint);
        waypoint.isPlaceable = false;
    }

    private void BreadthFirstSearch()
    {
        queue.Enqueue(startPoint);

        while(queue.Count > 0 && isRunning)
        {
            searchCenter = queue.Dequeue();
            searchCenter.isExplored = true;
            HaltIfEndFound();
            ExploreNeighbour();

        }
    }

    private void HaltIfEndFound()
    {
        if(searchCenter == endPoint)
        {
            isRunning = false;
        }
    }

    private void LoadBlocks()
    {
        Waypoint[] waypoints = FindObjectsOfType<Waypoint>();


        foreach(Waypoint waypoint in waypoints)
        {
            var gridPos = waypoint.GetGridPos();
            bool isOverlapping = grid.ContainsKey(gridPos);

            if (!isOverlapping && waypoint.accessible)
            {
                grid.Add(waypoint.GetGridPos(), waypoint);
            }    
        }
    }

    private void ExploreNeighbour()
    {
        if (!isRunning) { return; }

        foreach(Vector2Int direction in directions)
        {
            Vector2Int thisDirection = searchCenter.GetGridPos() + direction;
            if(grid.ContainsKey(thisDirection))
            {
                QueueNewNeighbour(thisDirection);
            }
        }
    }

    private void QueueNewNeighbour(Vector2Int thisDirection)
    {
        Waypoint neighbour = grid[thisDirection];
        if (neighbour.isExplored || queue.Contains(neighbour))
        {
            //do nothing
        }
        else
        {
            queue.Enqueue(neighbour);
            neighbour.exploredFrom = searchCenter;
        }
    }
}
