using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Singleton

    public static SoundManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    private AudioSource gameAudio;

    public AudioClip CannonFire;
    public AudioClip BombExplosion;
    public AudioClip BonusObtained;
    public AudioClip WallHit;
    public AudioClip FighterClash;

    private void Start()
    {
        gameAudio = GetComponent<AudioSource>();
    }

    private void PlaySound(AudioClip sound, float volume) => gameAudio.PlayOneShot(sound, volume);

    public void PlayCannonSound() => PlaySound(CannonFire, 1f);

    public void PlayBombSound() => PlaySound(BombExplosion, 1f);

    public void PlayBonusSound() => PlaySound(BonusObtained, 0.7f);

    public void PlayWallSound() => PlaySound(WallHit, 0.7f);

    public void PlayFighterSound() => PlaySound(FighterClash, 0.5f);
}
