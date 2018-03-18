using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

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
            if (!(destinationQueue.Count == 0))
            {
                MoveTo(destinationQueue[0]);
                destinationQueue.RemoveAt(0);
            }
        }
    }

    private int getFloorLogicDistanceToLastAddedDestination(Vector3 destination)
    {
        Vector3 basePosition;
        if (destinationQueue.Count == 0)
        {
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

    private Vector3 nextStepInPath(Vector3 currentPos, Vector3 destination)
    {
        GameObject[] floorSquares = GameObject.FindGameObjectsWithTag(Tags.FLOORSQUARE);

        // Cogemos los cuadrados a distancia 1
        List<GameObject> range1FloorSquares = new List<GameObject>();
        foreach(GameObject floorSquare in floorSquares)
        {
            if(utils.getFloorLogicDistance(currentPos, floorSquare.transform.position) == 1)
            {
                range1FloorSquares.Add(floorSquare);
            }
        }

        // Elegimos uno de ellos
        Vector3 currentBestDestinationFloorSquareCenter = destination;
        int currentMinFloorLogicDistance = 100;

        foreach(GameObject floorSquare in range1FloorSquares)
        {
            int currentFloorLogicDistance = utils.getFloorLogicDistance(currentPos, destination);
            if (currentFloorLogicDistance < currentMinFloorLogicDistance)
            {
                currentMinFloorLogicDistance = currentFloorLogicDistance;
                currentBestDestinationFloorSquareCenter = floorSquare.transform.position;
            }
        }

        return currentBestDestinationFloorSquareCenter;
    }

    private void CalculatePath (Vector3 destination)
    {
        int numSteps = getFloorLogicDistanceToLastAddedDestination(destination);
        Vector3 currentStep = transform.position;
        for(int i = 0; i<numSteps; i++)
        {
            if(utils.getFloorLogicDistance(currentStep, destination) > 1)
            {
                currentStep = nextStepInPath(currentStep, destination);
                destinationQueue.Add(currentStep);
            } else
            {
                destinationQueue.Add(destination);
            }

        }
    }

    public void SetDestination(Vector3 destination)
    {
        destinationQueue.Clear();
        if (getFloorLogicDistanceToLastAddedDestination(destination) == 1)
            destinationQueue.Add(destination);
        else
            CalculatePath(destination);
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
