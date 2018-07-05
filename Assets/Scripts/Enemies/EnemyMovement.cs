using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : ActorMovement {

    public int stepsPerTurn = 1;

    private Floor nextStepInPath(Floor from, Floor to)
    {
        GameObject[] floorSquares = GameObject.FindGameObjectsWithTag(Tags.FLOORSQUARE);

        // Cogemos los cuadrados a distancia 1
        List<Floor> range1FloorSquares = new List<Floor>();
        foreach (GameObject floorSquare in floorSquares)
        {
            if (utils.getFloorLogicDistance(from.Center, floorSquare.transform.position) == 1)
            {
                range1FloorSquares.Add(floorSquare.GetComponent<Floor>());
            }
        }

        // Elegimos uno de ellos, el que más nos acerque al destino
        Floor currentSelectedFloorSquare = range1FloorSquares[0];
        float currentMinFloorLogicDistance = 99999;

        foreach (Floor floorSquare in range1FloorSquares)
        {
            float currentFloorDistance = Vector3.Distance(floorSquare.Center, to.Center);
            if (currentFloorDistance < currentMinFloorLogicDistance)
            {
                currentMinFloorLogicDistance = currentFloorDistance;
                currentSelectedFloorSquare = floorSquare;
            }
        }

        return currentSelectedFloorSquare;
    }

    private Floor CalculateStep(Floor to)
    {
        Floor currentStep = currentFloor;

        for (int i = 0; i < stepsPerTurn; i++)
        {
            if (currentStep != to)
            {
                currentStep = nextStepInPath(currentStep, to);
            }
        }
        return currentStep;
    }

    public override void Move()
    {
        if (Destination == null) return;
        currentDestination = CalculateStep(Destination);
        AgentMoveTo(currentDestination);
    }

    public void PlayerMoving()
    {
        Move();
    }
}
