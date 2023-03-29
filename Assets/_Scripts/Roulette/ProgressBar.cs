using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Image Fill;
    public Image Background;
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

    public void FillOverDuration(float duration)
    {
        StartCoroutine(PowerUpTimer(duration));
    }

    IEnumerator PowerUpTimer(float duration)
    {
        float fillSpeed = 1 / duration;

        while (Fill.fillAmount < 1f)
        {
            Fill.fillAmount = Mathf.MoveTowards(Fill.fillAmount, 1, fillSpeed * Time.deltaTime);
            Debug.Log(Fill.fillAmount);
            yield return new WaitForEndOfFrame();
        }

        Fill.fillAmount = 0f;
    }

    public void ToggleProgressBar(bool state)
    {
        Background.gameObject.SetActive(state);
        Fill.gameObject.SetActive(state);
    }
}
