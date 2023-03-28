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

            if(PlayerCannon && !ReloadBoostActive && !MultiplierBoostActive && !Roulette.instance.Rolling)
            {
                if (coinProgress < CoinProgressGoal && !spinCharged)
                {
                    coinProgress++;
                }
                else if (coinProgress >= CoinProgressGoal)
                {
                    spinCharged = true;
                    //Roulette.instance.GetACoin();
                    Roulette.instance.ReactivateSpinOption();
                    coinProgress = 0;
                    CoinProgressBar.ResetProgress();
                    //CoinProgressBar.gameObject.SetActive(false);
                    //Debug.Log("Coin received!");
                }

                CoinProgressBar.AddProgress(progressBarSegment);
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
        CoinProgressBar.gameObject.SetActive(true);
        spinCharged = false;
        //Roulette.instance.ReactivateSpinOption();
    }
}
