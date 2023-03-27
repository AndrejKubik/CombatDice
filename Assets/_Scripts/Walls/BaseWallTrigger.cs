using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWallTrigger : MonoBehaviour
{
    public BaseWallCollumn WallCollumn;

    private void OnTriggerEnter(Collider other)
    {
        FighterCombat fighter = other.GetComponent<FighterCombat>();

        if (fighter)
        {
            WallCollumn.GetDamaged();
            Destroy(other.gameObject);
        }
    }
}
