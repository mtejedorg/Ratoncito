using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlayerMovement : ActorMovement {

    protected InputManager inputManager
    {
        get { return GameObject.FindGameObjectWithTag(Tags.GAMECONTROLLER).GetComponent<InputManager>(); }
    }

    public List<GameObject> Enemies
    {
        get
        {
            return new List<GameObject>(GameObject.FindGameObjectsWithTag(Tags.ENEMY));
        }
    }

    public override void SetDestination(Floor target)
    {
        if (utils.getFloorLogicDistance(currentFloor, target) == 1)
        {
            currentDestination = target;
            Move();
        }
    }

    public override void Move()
    {
        base.Move();
        foreach (GameObject enemy in Enemies)
        {
            enemy.SendMessage("PlayerMoving", SendMessageOptions.DontRequireReceiver);
        }
        inputManager.SendMessage("PlayerMoving", SendMessageOptions.DontRequireReceiver);
    }

    protected override void StepReached()
    {
        base.StepReached();
        currentFloor.ActivateFloor();
        foreach (GameObject enemy in Enemies)
        {
            enemy.SendMessage("PlayerMoved", SendMessageOptions.DontRequireReceiver);
        }
        inputManager.SendMessage("PlayerMoved", SendMessageOptions.DontRequireReceiver);
    }
}
