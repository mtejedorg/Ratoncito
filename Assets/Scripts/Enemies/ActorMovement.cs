using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ActorMovement : MonoBehaviour {

    protected NavMeshAgent agent;
    public float movementSpeed = 5.0f;
    public bool moving;
    public Floor currentFloor;
    protected Floor currentDestination;

    protected Utils utils
    {
        get { return GameObject.FindGameObjectWithTag(Tags.GAMECONTROLLER).GetComponent<Utils>(); }
    }

    public bool AbleToMove
    {
        get; set;
    }

    protected void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = movementSpeed;
        AbleToMove = true;
        if(currentFloor != null)
            transform.position = currentFloor.transform.position;
    }

    protected void Update()
    {
        float remainingDistance = Vector3.Distance(transform.position, agent.destination);
        if (remainingDistance < 1)
        {
            if (moving)
                StepReached();
        }
    }

    public Floor Destination
    {
        get; set;
    }

    public void SetDestination(GameObject dest)
    {
        SetDestination(dest.GetComponent<Floor>());
    }

    public virtual void SetDestination(Floor floor)
    {
        Destination = floor;
    }

    public virtual void Move()
    {
        AgentMoveTo(currentDestination);
    }

    protected virtual void StepReached()
    {
        moving = false;
        currentFloor = currentDestination;
        currentDestination = null;
    }

    public void AgentMoveTo(Floor to)
    {
        if (!moving && AbleToMove)
        {
            if (to == null) return;
            if (to == currentFloor) return;

            agent.destination = to.Center;
            moving = true;
        }
    }
}
