using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWallCollumn : MonoBehaviour
{
    private BaseWall wall;

    private void Start()
    {
        wall = transform.parent.GetComponent<BaseWall>();
    }

    public void GetDamaged()
    {
        transform.position += Vector3.down * wall.WallDamage;
    }
}
