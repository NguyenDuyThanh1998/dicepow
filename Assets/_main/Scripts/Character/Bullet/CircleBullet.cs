using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBullet : StraingBullet
{
    [SerializeField] float angle;

    private void Update()
    {
        var cur = direction.AngleInDeg();
        cur += angle * Time.deltaTime;
        SetDirection(cur);
        Launch();
    }
}
