using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterLauncher : MonoBehaviour
{
    public GameObject FighterPrefab;
    public Transform SpawnPointsParent;
    private List<Transform> spawnPoints = new List<Transform>();
    public bool PlayerCannon;
    public bool CanFire = true;
    public float ReloadTime;

    public bool ReloadBoostActive;
    public bool MultiplierBoostActive;

    public float MultiplierDuration;
    public ProgressBar PowerUpDurationBar;

    public int CoinProgressGoal;
    private int coinProgress;
    public ProgressBar CoinProgressBar;
    private float progressBarSegment;
    private bool spinCharged;

    public Animator CannonAnimator;

    private void Start()
    {
        for (int i = 0; i < SpawnPointsParent.childCount; i++)
        {
            spawnPoints.Add(SpawnPointsParent.GetChild(i));
        }

        //progressBarSegment = 1f / CoinProgressGoal;
        progressBarSegment = 1f / (CoinProgressGoal + 1);
        StartCoroutine(FighterLaunching());
    }

    private void SpawnFighters()
    {
        CannonAnimator.PlayInFixedTime("FighterCannonFire", 0, 0f);

        for (int i = 0; i < spawnPoints.Count; i++)
        {
            if(spawnPoints[i].gameObject.activeSelf)
            {
                Instantiate(FighterPrefab, spawnPoints[i].position, transform.rotation);
            }
        }
    }

    IEnumerator FighterLaunching()
    {
        while(CanFire)
        {
            SpawnFighters();
            
            if(PlayerCannon && !ReloadBoostActive && !MultiplierBoostActive && !Roulette.instance.Rolling && !Roulette.instance.Accelerating)
            {
                if (coinProgress < CoinProgressGoal && !spinCharged)
                {
                    coinProgress++;
                    CoinProgressBar.AddProgress(progressBarSegment);
                }
                else if (coinProgress >= CoinProgressGoal)
                {
                    //Roulette.instance.GetACoin();
                    Roulette.instance.ReactivateSpinOption();
                    coinProgress = 0;
                    CoinProgressBar.ResetProgress();
                    ToggleCoinProgressBar(false);
                    //Debug.Log("Coin received!");
                }
            }

            yield return new WaitForSeconds(ReloadTime);
        }
    }

    public void ToggleSpawnPoints(int count)
    {
        //show bonus shots left bar
        StartCoroutine(MultiSpawning(MultiplierDuration, count));
    }

    IEnumerator MultiSpawning(float duration, int count)
    {
        MultiplierBoostActive = true;
        PowerUpDurationBar.ToggleProgressBar(true);
        PowerUpDurationBar.FillOverDuration(duration);

        for (int i = 1; i < count; i++)
        {
            spawnPoints[i].gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(duration);

        for (int i = 1; i < count; i++)
        {
            spawnPoints[i].gameObject.SetActive(false);
        }

        MultiplierBoostActive = false;
        ToggleCoinProgressBar(true);
        PowerUpDurationBar.ToggleProgressBar(false);
        //Roulette.instance.ReactivateSpinOption();
    }

    public void BoostFireRate(float boostReloadTime, float boostDuration)
    {
        StartCoroutine(ReloadBoost(boostReloadTime, boostDuration));
    }

    IEnumerator ReloadBoost(float boostReloadTime, float boostDuration)
    {
        PowerUpDurationBar.ToggleProgressBar(true);
        float baseReload = ReloadTime;
        PowerUpDurationBar.FillOverDuration(boostDuration);
        ReloadBoostActive = true;
        ReloadTime *= boostReloadTime;

        yield return new WaitForSeconds(boostDuration);

        ReloadBoostActive = false;
        ReloadTime = baseReload;
        ToggleCoinProgressBar(true);
        PowerUpDurationBar.ToggleProgressBar(false);
        //Roulette.instance.ReactivateSpinOption();
    }

    public void ToggleCoinProgressBar(bool state)
    {
        CoinProgressBar.gameObject.SetActive(state);
        spinCharged = !state;
    }
}
