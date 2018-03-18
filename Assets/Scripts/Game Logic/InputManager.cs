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
                if (gameObjectHit.layer == Layers.FLOOR)
                {
                    print("Moviendo Player");
                    Vector3 destination = gameObjectHit.transform.position;
                    destination.y = player.transform.position.y;
                    player.GetComponent<PlayerMovement>().SetDestination(destination);
                }
            }
            if (Input.GetButtonDown(Action))
            {

                if (gameObjectHit.layer == Layers.FLOOR)
                {
                    print("Moviendo Enemigo");
                    Vector3 destination = gameObjectHit.transform.position;
                    destination.y = player.transform.position.y;
                    GameObject.FindGameObjectWithTag(Tags.ENEMY).GetComponent<EnemyMovement>().SetDestination(destination);
                }
            }
        }
    }
}