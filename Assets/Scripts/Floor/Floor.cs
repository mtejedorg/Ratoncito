using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {

	public Vector3 Center
    {
        get
        {
            return transform.position;
        }
    }

    public bool IsIlluminated
    {
        get; set;
    }

    public bool IsMouse
    {
        get; set;
    }

    public bool walkable = true;

    public virtual void ActivateFloor() { }
}
