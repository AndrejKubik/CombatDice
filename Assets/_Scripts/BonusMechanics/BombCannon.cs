using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class BombCannon : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform spawnPoint;
    private Vector3 center;
    [SerializeField] private Vector3 trajectoryRadius;

    private LineRenderer trajectory;
    private Vector3[] trajectoryPoints;
    [SerializeField, Min(2)] private int trajectoryDetail;

    public GameObject BombPrefab;

    private void Start()
    {
        trajectory = GetComponent<LineRenderer>();
        trajectoryPoints = new Vector3[trajectoryDetail];
    }

    private void Update()
    {
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        ShowAimTrajectory();

        if(Input.GetKeyDown(KeyCode.Space)) LaunchProjectile();
    }

    private void ShowAimTrajectory()
    {
        center = (target.position + spawnPoint.position) * 0.5f;
        center -= trajectoryRadius;

        for (int i = 0; i < trajectoryDetail; i++)
        {
            float t = (float)i / trajectoryDetail;
            Vector3 currentPoint = Vector3.Slerp(spawnPoint.position - center, target.position - center, t);
            trajectoryPoints[i] = currentPoint + center;
        }

        trajectory.positionCount = trajectoryDetail;
        trajectory.SetPositions(trajectoryPoints);
    }

    public void LaunchProjectile()
    {
        GameObject projectile = Instantiate(BombPrefab, spawnPoint.position, spawnPoint.rotation);
        AimedProjectile projectileData = projectile.GetComponent<AimedProjectile>();
        projectileData.SpawnPoint = spawnPoint;
        projectileData.Target = target;
        projectileData.TrajectoryRadius = trajectoryRadius;
        projectileData.ProjectileDataLoaded = true;
        transform.parent.gameObject.SetActive(false);
        Roulette.instance.ReactivateSpinOption();
    }
}
