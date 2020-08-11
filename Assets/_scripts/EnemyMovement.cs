using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    Waypoint endPoint;

    Vector3 targetPos;

    void Start()
    {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        var path = pathfinder.GetPath();

        StartCoroutine(FollowPath(path));
    }

    private void Update()
    {
        if (transform.position == endPoint.transform.position)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
        }
    }

    IEnumerator FollowPath(List<Waypoint> path)
    {
        endPoint = path[path.Count - 1];

        foreach (Waypoint block in path)
        {
            targetPos = block.transform.position;

            yield return new WaitUntil(() => transform.position == targetPos);
        }
        GetComponent<Health>().Explode();
    }
}
