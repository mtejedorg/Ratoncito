using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoisyFloor : Floor {

	public override void ActivateFloor()
    {
        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag(Tags.ENEMY))
        {
            enemy.SendMessage("NoisyAlert", this, SendMessageOptions.DontRequireReceiver);
        }
    }
}
