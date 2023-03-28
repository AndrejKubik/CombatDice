using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class BonusCannon : MonoBehaviour
{
    [SerializeField] private GameObject fighterCannonModel;
    public FighterLauncher FighterLauncher;

    public Transform BombTarget;
    public Transform SuperBombTarget;
    public Transform Multiplier2Target;
    public Transform Multiplier5Target;

    public Transform ActiveTarget;
    [SerializeField] private Transform spawnPoint;
    private Vector3 center;
    [SerializeField] private Vector3 trajectoryRadius;

    private LineRenderer trajectory;
    private Vector3[] trajectoryPoints;
    [SerializeField, Min(2)] private int trajectoryDetail;

    public GameObject ProjectilePrefab;

    public Transform ParticleHolder;
    private ParticleSystem spawnParticles;

    [Header("Insert particle:")]
    public GameObject SpawnParticlesPrefab;

    private void Start()
    {
        trajectory = GetComponent<LineRenderer>();
        trajectoryPoints = new Vector3[trajectoryDetail];

        GameObject newParticles = Instantiate(SpawnParticlesPrefab, ParticleHolder.position, transform.rotation, ParticleHolder);
        spawnParticles = newParticles.GetComponent<ParticleSystem>();
        spawnParticles.Play();
    }

    private void OnEnable()
    {
        if (spawnParticles) spawnParticles.Play();
    }

    private void Update()
    {
        if(ActiveTarget)
        {
            transform.LookAt(new Vector3(ActiveTarget.position.x, transform.position.y, ActiveTarget.position.z));
            ShowAimTrajectory();
        }
    }

    private void ShowAimTrajectory()
    {
        center = (ActiveTarget.position + spawnPoint.position) * 0.5f;
        center -= trajectoryRadius;

        for (int i = 0; i < trajectoryDetail; i++)
        {
            float t = (float)i / trajectoryDetail;
            Vector3 currentPoint = Vector3.Slerp(spawnPoint.position - center, ActiveTarget.position - center, t);
            trajectoryPoints[i] = currentPoint + center;
        }

        trajectory.positionCount = trajectoryDetail;
        trajectory.SetPositions(trajectoryPoints);
    }

    public void LaunchProjectile()
    {
        GameObject projectile = Instantiate(ProjectilePrefab, spawnPoint.position, spawnPoint.rotation);
        AimedProjectile projectileData = projectile.GetComponent<AimedProjectile>();
        projectileData.SpawnPoint = spawnPoint;
        projectileData.Target = ActiveTarget;
        projectileData.TrajectoryRadius = trajectoryRadius;
        projectileData.ProjectileDataLoaded = true;
        transform.parent.gameObject.SetActive(false);
        Roulette.instance.ReactivateSpinOption();
        ActiveTarget.gameObject.SetActive(false);
        ToggleFighterLauncher(true);
    }

    public void LoadCannon(GameObject projectilePrefab, Transform target)
    {
        ToggleFighterLauncher(false);
        transform.parent.gameObject.SetActive(true);
        ProjectilePrefab = projectilePrefab;
        target.gameObject.SetActive(true);
        ActiveTarget = target;
    }

    private void ToggleFighterLauncher(bool state)
    {
        fighterCannonModel.SetActive(state);
        FighterLauncher.enabled = state;
    }
}
