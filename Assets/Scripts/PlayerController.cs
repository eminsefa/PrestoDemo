using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    public float speed = 5f;
    public bool magnetCanMove = false;
    GameObject movableMagnet;
    public static PlayerController instance;
    Vector3 mousePosWorld;
    Vector3 mousePos;
    void Start()
    {
        instance = this;
        cam.orthographicSize = (13f * Screen.height / Screen.width) * 0.5f;
    }

    void FixedUpdate()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos = new Vector3(touch.position.x, touch.position.y, 20);
            Vector3 touchPosWorld = cam.ScreenToWorldPoint(touchPos);
            Vector3 direction = touchPosWorld - cam.transform.position;

            if (touch.phase == TouchPhase.Began)
            {
                RaycastHit hit;
                if (Physics.Raycast(cam.transform.position, direction, out hit, LayerMask.GetMask("DragTargets")))
                {
                    if(hit.collider.gameObject.layer!=10) //Double check for bug
                    {
                        return;
                    }
                        magnetCanMove = true;
                        movableMagnet = hit.collider.gameObject;   
                }
            }
            if(touch.phase==TouchPhase.Moved)
            {
                if(magnetCanMove)
                {
                    Vector3 moveDirection = new Vector3(touchPosWorld.x, movableMagnet.transform.position.y, touchPosWorld.z)- movableMagnet.transform.position;
                    //movableMagnet.GetComponent<Rigidbody>().AddRelativeForce(moveDirection * speed*Time.fixedDeltaTime,ForceMode.Impulse);
                    //Choosed velocity for smooth movement,both works
                    movableMagnet.GetComponent<Rigidbody>().velocity = Vector3.MoveTowards(movableMagnet.transform.position, moveDirection,speed); 
                }
            }
            if(touch.phase==TouchPhase.Ended || touch.phase==TouchPhase.Canceled)
            {               
                magnetCanMove = false;
                movableMagnet.GetComponent<Rigidbody>().velocity = Vector3.zero;
                movableMagnet = null;
            }
        }

        //Below for mouse input

        if (Input.GetMouseButtonDown(0))
        {
            mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20);
            mousePosWorld = cam.ScreenToWorldPoint(mousePos);
            Vector3 rayDirection = mousePosWorld - cam.transform.position;

            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, rayDirection, out hit, LayerMask.GetMask("DragTargets")))
            {
                if (hit.collider.gameObject.layer != 10)
                {
                    return;
                }
                magnetCanMove = true;
                movableMagnet = hit.collider.gameObject;
            }
        }
        if(Input.GetMouseButton(0))
        {
            if (magnetCanMove)
            {
                mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20);
                mousePosWorld = cam.ScreenToWorldPoint(mousePos);
                Vector3 moveDirection = new Vector3(mousePosWorld.x, movableMagnet.transform.position.y, mousePosWorld.z) - movableMagnet.transform.position;
                movableMagnet.GetComponent<Rigidbody>().velocity = Vector3.MoveTowards(movableMagnet.transform.position, moveDirection, speed);
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            movableMagnet = null;            
        }
    }
}
