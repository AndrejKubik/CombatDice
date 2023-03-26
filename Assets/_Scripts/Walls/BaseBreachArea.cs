using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBreachArea : MonoBehaviour
{
    public Transform King;

    private void OnTriggerEnter(Collider other)
    {
        FighterMovement fighter = other.GetComponent<FighterMovement>();

        if(fighter)
        {
            fighter.InEnemyBase = true;
            fighter.AttackTarget = King;
            fighter.AttackTheTarget();
        }
    }
}
