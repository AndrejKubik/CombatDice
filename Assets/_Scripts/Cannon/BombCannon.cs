using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class BombCannon : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform spawnPoint;
    private Vector3 center;

    private LineRenderer trajectory;
    private Vector3[] trajectoryPoints;
    [SerializeField, Min(2)] private int trajectoryDetail;

    private void Start()
    {
        trajectory = GetComponent<LineRenderer>();
        trajectoryPoints = new Vector3[trajectoryDetail];
    }

    private void Update()
    {
        //transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        center = (target.position + spawnPoint.position) * 0.5f;
        center -= Vector3.up;

        for (int i = 0; i < trajectoryDetail; i++)
        {
            float t = (float)i / trajectoryDetail;
            Vector3 currentPoint = Vector3.Slerp(spawnPoint.position - center, target.position - center, t);
            trajectoryPoints[i] = currentPoint + center;
        }

        trajectory.positionCount = trajectoryDetail;
        trajectory.SetPositions(trajectoryPoints);
    }
}
