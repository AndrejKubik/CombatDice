using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FighterCombat : MonoBehaviour
{
    public bool PlayerTeam;
    public bool Targeted;

    public Transform ParticleHolder;
    public ParticleSystem ClashParticles;

    [Header("Insert particle:")]
    public GameObject ClashParticlesPrefab;

    private void Start()
    {
        GameObject newParticle = Instantiate(ClashParticlesPrefab, ParticleHolder.position, transform.rotation, ParticleHolder);
        ClashParticles = newParticle.GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        FighterCombat fighter = other.GetComponent<FighterCombat>();

        if (fighter != null)
        {
            if(!fighter.PlayerTeam && PlayerTeam)
            {
                SoundManager.instance.PlayFighterSound();
                KillStickman(fighter);
                KillStickman(this);
            }
        }
    }

    private void KillStickman(FighterCombat stickman)
    {
        stickman.transform.DOKill();
        if (stickman.ClashParticles)
        {
            stickman.ClashParticles.Play();
            stickman.ClashParticles.transform.SetParent(null);
        }
        Destroy(stickman.gameObject);
    }
}
