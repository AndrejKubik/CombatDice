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
    private Vector3 aimedPosition;
    private Vector3 launchPosition;
    public Vector3 TrajectoryRadius;

    private BonusCannonTarget targetMarker;
    public FighterLauncher PlayerCannon;
    public bool ProjectileDataLoaded;

    public enum ProjectileType { Bomb, SuperBomb, Multiplier2, Multiplier5 }
    [SerializeField] private ProjectileType projectileType;
    [SerializeField] private GameObject multiplierPrefab;

    private ParticleSystem impactParticles;

    [Header("Insert particle:")]
    public GameObject ImpactParticlesPrefab;

    private void Start()
    {
        targetMarker = Target.GetComponent<BonusCannonTarget>();
        aimedPosition = Target.position;
        launchPosition = SpawnPoint.position;

        //GameObject newParticles = Instantiate(ImpactParticlesPrefab, transform.position, transform.rotation, transform);
        //impactParticles = newParticles.GetComponent<ParticleSystem>();
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
        currentDistance += Time.unscaledDeltaTime;
        t = currentDistance / ProjectileSpeed;
        t = Mathf.Clamp01(t);
        trajectoryCenter = (launchPosition + aimedPosition) * 0.5f;
        trajectoryCenter -= TrajectoryRadius;
        Vector3 currentPosition = Vector3.Slerp(launchPosition - trajectoryCenter, aimedPosition - trajectoryCenter, t);
        transform.position = currentPosition + trajectoryCenter;
    }

    private void ActivateEffect()
    {
        Vector3 particleSpawnPosition = new Vector3(transform.position.x, 0.5f, transform.position.z);
        Instantiate(ImpactParticlesPrefab, particleSpawnPosition, transform.rotation);
        Time.timeScale = 1f;
        
        PlayerCannon.ToggleCoinProgressBar(true);
        CameraControl.instance.ToggleSlowMotion(false);

        switch (projectileType)
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
        SoundManager.instance.PlayBombSound();

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
        Instantiate(multiplierPrefab, aimedPosition, multiplierPrefab.transform.rotation);
        Destroy(gameObject);
    }
}
