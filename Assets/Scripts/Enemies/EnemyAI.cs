using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    private GameObject player;
    private Utils utils;

    public int maxHearRange = 3;
    public int maxVisualRange = 2;

    private ActorMovement characterMovement;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
        utils = GameObject.FindGameObjectWithTag(Tags.GAMECONTROLLER).GetComponent<Utils>();
        characterMovement = GetComponent<ActorMovement>();
    }

    public bool inHearRange(Floor floor)
    {
        return utils.getFloorLogicDistance(transform.position, floor.Center) <= maxHearRange;
    }

    public void NoisyAlert(Floor floor)
    {
        if (inHearRange(floor))
            characterMovement.SetDestination(floor);

    }

}
