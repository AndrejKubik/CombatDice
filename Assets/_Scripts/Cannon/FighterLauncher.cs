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

    public int CoinProgressGoal;
    private int coinProgress;
    public CoinProgressBar ProgressBar;
    private float progressBarSegment;

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
    IEnumerator FighterLaunching()
    {
        while(CanFire)
        {
            SpawnFighters();

            if(PlayerCannon && !ReloadBoostActive)
            {
                if (coinProgress < CoinProgressGoal)
                {
                    coinProgress++;
                }
                else if (coinProgress >= CoinProgressGoal)
                {
                    Roulette.instance.GetACoin();
                    coinProgress = 0;
                    Debug.Log("Coin received!");
                }

                ProgressBar.AddProgress(progressBarSegment);
            }

            yield return new WaitForSeconds(ReloadTime);
        }
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

    public void SwitchSpawnPoints(bool state, int count)
    {
        for (int i = 1; i < count; i++)
        {
            spawnPoints[i].gameObject.SetActive(state);
        }
    }
}
