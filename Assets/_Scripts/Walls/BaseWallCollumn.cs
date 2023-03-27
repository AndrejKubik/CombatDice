using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWallCollumn : MonoBehaviour
{
    private BaseWall wall;
    public Transform Bricks;
    public Transform ParticleHolder;
    private ParticleSystem demolishParticle;

    [Header("Insert particle:")]
    public GameObject DemolishParticlePrefab;

    private void Start()
    {
        wall = transform.parent.GetComponent<BaseWall>();
        GameObject newParticle = Instantiate(DemolishParticlePrefab, ParticleHolder.position, transform.rotation, ParticleHolder);
        demolishParticle = newParticle.GetComponent<ParticleSystem>();
    }

    public void GetDamaged()
    {
        if (demolishParticle) demolishParticle.Play();
        Bricks.position += Vector3.down * wall.WallDamage;
    }
}
