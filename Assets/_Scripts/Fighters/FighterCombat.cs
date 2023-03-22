using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterCombat : MonoBehaviour
{
    public bool PlayerTeam;
    public bool Targeted;

    private void OnTriggerEnter(Collider other)
    {
        FighterCombat fighter = other.GetComponent<FighterCombat>();

        if (fighter != null)
        {
            if(!fighter.PlayerTeam && PlayerTeam)
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    FighterCombat fighter = collision.transform.GetComponent<FighterCombat>();

    //    if (fighter != null && !fighter.PlayerTeam)
    //    {
    //        Destroy(fighter.gameObject);
    //        Destroy(gameObject);
    //    }
    //}
}
