using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimedProjectile : MonoBehaviour
{
    public float ProjectileSpeed;
    private float currentDistance;
    private float t;
    private Vector3 trajectoryCenter;

    public Transform SpawnPoint;
    public Transform Target;
    public Vector3 TrajectoryRadius;

    private BombCannonTarget targetMarker;
    public bool ProjectileDataLoaded;

    private void Start()
    {
        targetMarker = Target.GetComponent<BombCannonTarget>();
    }

    private void Update()
    {
        if (ProjectileDataLoaded)
        {
            FlyAlongTrajectory();

            if (t >= 1f) Explode();
        }
    }

    private void FlyAlongTrajectory()
    {
        currentDistance += Time.deltaTime;
        t = currentDistance / ProjectileSpeed;
        t = Mathf.Clamp01(t);
        trajectoryCenter = (SpawnPoint.position + Target.position) * 0.5f;
        trajectoryCenter -= TrajectoryRadius;
        Vector3 currentPosition = Vector3.Slerp(SpawnPoint.position - trajectoryCenter, Target.position - trajectoryCenter, t);
        transform.position = currentPosition + trajectoryCenter;
    }

    private void Explode()
    {
        for (int i = 0; i < targetMarker.UnitsInRange.Count; i++)
        {
            Destroy(targetMarker.UnitsInRange[i]);
        }

        Destroy(gameObject);
    }
}
