using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterLauncher : MonoBehaviour
{
    public GameObject FighterPrefab;
    public Transform SpawnPointsParent;
    private List<Transform> spawnPoints = new List<Transform>();
    public bool CanFire = true;
    public float ReloadTime;
    public bool PlayerCannon;
    public int CoinProgressGoal;
    private int coinProgress;

    private void Start()
    {
        for (int i = 0; i < SpawnPointsParent.childCount; i++)
        {
            spawnPoints.Add(SpawnPointsParent.GetChild(i));
        }

        StartCoroutine(FighterLaunching());
    }
    IEnumerator FighterLaunching()
    {
        while(CanFire)
        {
            SpawnFighters();

            if(PlayerCannon)
            {
                if (coinProgress < CoinProgressGoal) coinProgress++;
                else if (coinProgress >= CoinProgressGoal)
                {
                    Roulette.instance.GetACoin();
                    coinProgress = 0;
                    Debug.Log("Coin received!");
                }
            }

            yield return new WaitForSeconds(ReloadTime);
        }
    }

    private void SpawnFighters()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            if(spawnPoints[i].gameObject.activeSelf)
            {
                Instantiate(FighterPrefab, spawnPoints[i].position, transform.rotation);
            }
        }
    }
}
