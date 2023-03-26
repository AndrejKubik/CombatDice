using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FighterMovement : MonoBehaviour
{
    public bool InEnemyBase;
    public Transform AttackTarget;
    public float AttackDuration;
    public Ease AttackMovementCurve;

    public float RunSpeed = 10f;

    public bool Multiplied;
    [SerializeField] private GameObject multiplyPrefab;
    [SerializeField, Range(0f, 1f)] private float multiplyDistance;
    [SerializeField, Range(0f, 1f)] private float multiplyRadius;


    private void Update()
    {
        if (!InEnemyBase) RunForward();
        else LockOnTarget();
    }

    private void RunForward()
    {
        transform.Translate(Vector3.forward * RunSpeed * Time.deltaTime);
    }

    private void LockOnTarget()
    {
        transform.LookAt(AttackTarget, Vector3.up);
    }

    public void AttackTheTarget()
    {
        King enemyKing = AttackTarget.GetComponent<King>();
        transform.DOMove(AttackTarget.position, AttackDuration).SetEase(AttackMovementCurve)
            .OnComplete(() =>
        {
            enemyKing.GetDamaged(1);
            Destroy(gameObject);
        });
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
