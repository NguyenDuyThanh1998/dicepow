using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameConst
{
    public const float Vertical = 1f;
    public const float Horizontal = 1f;
    public static Vector2 GetWorld => new Vector2(Horizontal, Vertical);

    public static Vector2 GetVectorInWorld(this Vector2 velo)
    {
        velo.x *= Horizontal;
        velo.y *= Vertical;
        return velo;
    }

    public static Vector3 GetVectorInWorld(this Vector3 velo)
    {
        velo.x *= Horizontal;
        velo.y *= Vertical;
        return new Vector3(velo.x, velo.y,1);
    }

    public static bool CheckInLimit(this Vector2 velo, float target)
    {
        return target > velo.x && target < velo.y ;
    }

    public static float AngleInDeg(this Vector2 dir)
    {
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }

    public static Vector2 Angle2Direction(this float angle)
    {
        Vector2 value;
        var angleInRad = Mathf.Deg2Rad * angle;
        value.x = Mathf.Cos(angleInRad);
        value.y = Mathf.Sin(angleInRad);
        return value;
    }

    //public static Vector3 Angle2Direction(this float angle)
    //{
    //    Vector3 value;
    //    var angleInRad = Mathf.Deg2Rad * angle;
    //    value.x = Mathf.Cos(angleInRad);
    //    value.y = Mathf.Sin(angleInRad);
    //    value.z = 1;
    //    return value;
    //}

    public static int RandomInRange(this Vector2Int dir)
    {
        return Random.Range(dir.x, dir.y);
    }

    public static float RandomInRange(this Vector2 dir)
    {
        return Random.Range(dir.x,dir.y);
    }
}
