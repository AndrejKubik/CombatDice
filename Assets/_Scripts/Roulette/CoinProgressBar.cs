using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinProgressBar : MonoBehaviour
{
    public Image Fill;
    private float currentProgress;
    public float FillSpeed;

    private void Update()
    {
        Fill.fillAmount = Mathf.MoveTowards(Fill.fillAmount, currentProgress, FillSpeed * Time.deltaTime);

        if (Fill.fillAmount == 1f) ResetProgress();
    }

    public void AddProgress(float amount) => currentProgress += amount;

    public void ResetProgress()
    {
        currentProgress = 0f;
        Fill.fillAmount = 0f;
    }
}
