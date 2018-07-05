using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour {

    public float floorSize = 12;

    public float getDiagonal()
    {
        return Mathf.Sqrt(2 * Mathf.Pow(floorSize / 2, 2));
    }

    public int getFloorLogicDistance(Floor fromFloor, Floor toFloor)
    {
        return getFloorLogicDistance(fromFloor.Center, toFloor.Center);
    }

    public int getFloorLogicDistance(Vector3 basePosition, Vector3 floorSquareCenter)
    {
        return Mathf.FloorToInt(Vector3.Distance(floorSquareCenter, basePosition) / getDiagonal());
    }
}
