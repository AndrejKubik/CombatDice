using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CannonMovement : MonoBehaviour
{
    public bool Player;
    public float MoveRange = 5f;

    private bool moving;
    private Vector3 computerTargetPosition;
    public float ComputerMoveDuration;
    public Ease EaseType;

    private void Update()
    {
        if (!Player) MoveComputerCannon();
    }

    private void OnMouseDrag() 
    {
        if (Player) FollowTheMouse();
    }

    private void FollowTheMouse()
    {
        Vector3 targetPosition = new Vector3(MouseWorldPosition().x, transform.position.y, transform.position.z);

        if (targetPosition.x >= -MoveRange && targetPosition.x <= MoveRange)
        {
            transform.position = targetPosition;
        }
    }

    private Vector3 MouseWorldPosition()
    {
        Vector3 rawMousePosition = Input.mousePosition;
        rawMousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z;

        return Camera.main.ScreenToWorldPoint(rawMousePosition);
    }

    private void MoveComputerCannon()
    {
        if (!moving)
        {
            moving = true;
            ChooseRandomDestination();
            transform.DOMove(computerTargetPosition, ComputerMoveDuration).SetEase(EaseType).OnComplete(() => { moving = false; });
        }
    }

    private void ChooseRandomDestination()
    {
        float randomX = Random.Range(-MoveRange, MoveRange);
        computerTargetPosition = new Vector3(randomX, transform.position.y, transform.position.z);
        //Debug.Log(computerTargetPosition);
    }
}
