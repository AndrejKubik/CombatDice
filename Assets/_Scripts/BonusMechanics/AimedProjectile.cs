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

    private BonusCannonTarget targetMarker;
    public bool ProjectileDataLoaded;

    public enum ProjectileType { Bomb, SuperBomb, Multiplier2, Multiplier5 }
    [SerializeField] private ProjectileType projectileType;
    [SerializeField] private GameObject multiplierPrefab;

    private void Start()
    {
        targetMarker = Target.GetComponent<BonusCannonTarget>();
    }

    private void Update()
    {
        if (ProjectileDataLoaded)
        {
            FlyAlongTrajectory();
            if (t >= 1f) ActivateEffect();
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

    private void ActivateEffect()
    {
        switch(projectileType)
        {
            case ProjectileType.Bomb:
                Explode();
                break;

            case ProjectileType.SuperBomb:
                ExplodeSuper();
                break;

            case ProjectileType.Multiplier2:
                PlaceMultiplier();
                break;

            case ProjectileType.Multiplier5:
                PlaceMultiplier();
                break;
        }
    }

    private void Explode()
    {
        for (int i = 0; i < targetMarker.UnitsInRange.Count; i++)
        {
            Destroy(targetMarker.UnitsInRange[i]);
        }

        Destroy(gameObject);
    }

    private void ExplodeSuper()
    {
        for (int i = 0; i < targetMarker.UnitsInRange.Count; i++)
        {
            Destroy(targetMarker.UnitsInRange[i]);
        }

        Destroy(gameObject);
    }

    private void PlaceMultiplier()
    {
        Instantiate(multiplierPrefab, targetMarker.transform.position, multiplierPrefab.transform.rotation);
        Destroy(gameObject);
    }
}
