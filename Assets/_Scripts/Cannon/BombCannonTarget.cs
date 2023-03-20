using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCannonTarget : MonoBehaviour
{
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
}
