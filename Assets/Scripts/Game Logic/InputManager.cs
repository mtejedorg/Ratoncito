using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputManager : MonoBehaviour
{

    public enum Actions { Press, Drag, Hold, Release }
    public Actions input;

    public const string Press = "Fire1";
    public const string Action = "Fire2";

    private Camera cam;
    private Ray mousePositionRay;

    public GameObject player;

    private bool isPlayerMoving = false;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag(Tags.MAINCAMERA).GetComponent<Camera>(); ;
    }

    void Update()
    {
        mousePositionRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        GameObject gameObjectHit;
        if (Physics.Raycast(mousePositionRay, out hit, Mathf.Infinity))
        {
            gameObjectHit = hit.transform.gameObject;

            if (Input.GetButtonDown(Press))
            {
                if (isPlayerMoving) return;
                if (gameObjectHit.layer == Layers.FLOOR)
                {
                    player.GetComponent<PlayerMovement>().SetDestination(gameObjectHit.GetComponent<Floor>());
                }
            }
            if (Input.GetButtonDown(Action))
            {

                if (gameObjectHit.layer == Layers.FLOOR)
                {
                    GameObject.FindGameObjectWithTag(Tags.ENEMY).GetComponent<EnemyMovement>().SetDestination(gameObjectHit);
                }
            }
        }
    }

    public void PlayerMoving()
    {
        isPlayerMoving = true;
    }

    public void PlayerMoved()
    {
        isPlayerMoving = false;
    }
}