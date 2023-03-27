using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWallTrigger : MonoBehaviour
{
    private BaseWallCollumn wallCollumn;

    private void Start()
    {
        wallCollumn = transform.parent.GetComponent<BaseWallCollumn>();
    }

    private void OnTriggerEnter(Collider other)
    {
        FighterCombat fighter = other.GetComponent<FighterCombat>();

        if (fighter)
        {
            wallCollumn.GetDamaged();
            Destroy(other.gameObject);
        }
    }
}
