using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    private GameObject player;
    private Utils utils;

    public int maxHearRange = 3;
    public int maxVisualRange = 2;

    private PlayerMovement characterMovement;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
        utils = GameObject.FindGameObjectWithTag(Tags.GAMECONTROLLER).GetComponent<Utils>();
        characterMovement = GetComponent<PlayerMovement>();
    }

    public void NoisyAlert(Vector3 floorSquareCenter)
    {
        if (utils.getFloorLogicDistance(transform.position, floorSquareCenter) <= maxHearRange)
            characterMovement.SetDestination(floorSquareCenter);

    }

}
