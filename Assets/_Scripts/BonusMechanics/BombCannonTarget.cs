using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCannonTarget : MonoBehaviour
{
    public List<GameObject> UnitsInRange;

    private void OnMouseDrag()
    {
        transform.position = new Vector3(MouseWorldPosition().x, transform.position.y, MouseWorldPosition().z);
    }

    private Vector3 MouseWorldPosition()
    {
        Vector3 rawMousePosition = Input.mousePosition;
        rawMousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z;

        return Camera.main.ScreenToWorldPoint(rawMousePosition);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3)
        {
            FighterCombat fighter = other.GetComponent<FighterCombat>();
            if(!fighter.PlayerTeam) UnitsInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            FighterCombat fighter = other.GetComponent<FighterCombat>();
            if (!fighter.PlayerTeam) UnitsInRange.Remove(other.gameObject);
        }
    }
}
