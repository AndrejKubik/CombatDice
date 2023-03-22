using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FighterMovement : MonoBehaviour
{
    public float RunSpeed = 10f;
    public bool Multiplied;
    [SerializeField, Range(0f, 1f)] private float multiplyDistance;
    [SerializeField, Range(0f, 1f)] private float multiplyRadius;
    [SerializeField] private GameObject multiplyPrefab;

    private void Update()
    {
        RunForward();
    }

    private void OnDisable()
    {
        transform.DOKill();
    }

    private void RunForward()
    {
        transform.Translate(Vector3.forward * RunSpeed * Time.deltaTime);
    }

    public void Multiply(int multiplierNumber)
    {
        if (!Multiplied)
        {
            for (int i = 0; i < multiplierNumber; i++)
            {
                float xPosition = multiplyDistance * Mathf.Sqrt(i) * Mathf.Cos(i * multiplyRadius);
                float yPosition = multiplyDistance * Mathf.Sqrt(i) * Mathf.Sin(i * multiplyRadius);

                Vector3 newPosition = transform.position + new Vector3(xPosition, 0f, yPosition);

                GameObject clone = Instantiate(multiplyPrefab, transform.position, transform.rotation);
                clone.transform.DOMove(newPosition, 0.15f).SetEase(Ease.OutBack).OnComplete(() => transform.DOKill());
                clone.GetComponent<FighterMovement>().Multiplied = true;
            }
            
            Multiplied = true;
        }
    }

    public void UnblockMultiplication()
    {
        StartCoroutine(MultiplierCooldown());
    }

    IEnumerator MultiplierCooldown()
    {
        yield return new WaitForSeconds(1f);
        Multiplied = false;
    }
}
