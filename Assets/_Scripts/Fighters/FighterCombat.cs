using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
                other.transform.DOKill();
                Destroy(other.gameObject);
                transform.DOKill();
                Destroy(gameObject);
            }
        }
    }
}
