using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlayerMovement : MonoBehaviour {

    private NavMeshAgent agent;
    public float movementSpeed = 5.0f;
    public bool moving;
    private List<Vector3> destinationQueue = new List<Vector3>();

    private Utils utils
    {
        get { return GameObject.FindGameObjectWithTag(Tags.GAMECONTROLLER).GetComponent<Utils>(); }
    }

    private bool ableToMove = true;
    public bool AbleToMove
    {
        get { return ableToMove; }
        set { ableToMove = value; }
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = movementSpeed;
    }

    private void Update()
    {
        float remainingDistance = Vector3.Distance(transform.position, agent.destination);
        print(remainingDistance);
        if (remainingDistance < 1)
        {
            moving = false;
            if(!(destinationQueue.Count == 0))
            {
                MoveTo(destinationQueue[0]);
                destinationQueue.RemoveAt(0);
            }
        }
    }

    private int getFloorLogicDistanceToLastAddedDestination(Vector3 destination)
    {
        Vector3 basePosition;
        if (destinationQueue.Count == 0) {
            if (agent.isStopped)
                basePosition = transform.position;
            else
                basePosition = agent.destination;
        }
        else
        {
            basePosition = destinationQueue[destinationQueue.Count - 1];
        }
        return utils.getFloorLogicDistance(basePosition, destination);
    }

    public void SetDestination(Vector3 destination)
    {
        if(getFloorLogicDistanceToLastAddedDestination(destination) == 1)
            destinationQueue.Add(destination);
    }

    public void MoveTo(Vector3 destination)
    {
        if (!moving && ableToMove)
        {
            agent.destination = destination;
            moving = true;
        }
    }
}
