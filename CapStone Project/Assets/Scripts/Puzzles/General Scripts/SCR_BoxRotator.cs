using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_BoxRotator : MonoBehaviour
{
    public GameObject camTransformX;
    public GameObject camTransformY;
    public GameObject yChange;
    public GameObject xChange;
    public float zoomSpeed;

    public float turnSpeed;
    public float distance;

    public bool on = false;

    // Update is called once per frame
    void Update()
    {
        if (!on) return;
        //rotation
        if (Input.GetMouseButton(1))
        {
            //works in scene were box is center
            /*if (camTransformY.transform.position.z >= 0)
            {
                camTransformY.transform.RotateAround(transform.position,camTransformY.transform.right,-Input.GetAxis("Mouse Y") * turnSpeed);
            }
            else
            {
                camTransformY.transform.RotateAround(transform.position,-camTransformY.transform.right,-Input.GetAxis("Mouse Y") * turnSpeed);
            }
            if (camTransformY.transform.position.z >= 0)
            {
                camTransformX.transform.RotateAround(transform.position,camTransformX.transform.up,-Input.GetAxis("Mouse X") * turnSpeed);
            }
            else
            {
                camTransformX.transform.RotateAround(transform.position,-camTransformX.transform.up,-Input.GetAxis("Mouse X") * turnSpeed);
            }*/
            
            if (camTransformY.transform.position.z >= transform.position.z && camTransformY.transform.position.x >= transform.position.x )
            { 
                camTransformY.transform.RotateAround(transform.position,camTransformY.transform.right,-Input.GetAxis("Mouse Y") * turnSpeed);
            }
            else
            {
                camTransformY.transform.RotateAround(transform.position,-camTransformY.transform.right,-Input.GetAxis("Mouse Y") * turnSpeed);
            }
            if (camTransformY.transform.position.z >= transform.position.z && camTransformY.transform.position.x >= transform.position.x)
            {
                camTransformX.transform.RotateAround(transform.position,camTransformX.transform.up,-Input.GetAxis("Mouse X") * turnSpeed);
            }
            else
            {
                camTransformX.transform.RotateAround(transform.position,-camTransformX.transform.up,-Input.GetAxis("Mouse X") * turnSpeed);
            }
            
            if (Vector3.Distance(camTransformX.transform.position, transform.position) > distance + 1)
            {
                camTransformX.transform.position = Vector3.Lerp(camTransformX.transform.position, this.transform.position, zoomSpeed);
            }
            if (Vector3.Distance(camTransformY.transform.position, transform.position) > distance + 1)
            {
                camTransformY.transform.position = Vector3.Lerp(camTransformY.transform.position, this.transform.position, zoomSpeed);
            }
            
            
        }

        if (Input.GetMouseButtonUp(1))
        {
        }
        if (Input.GetMouseButtonDown(1))
        {
            //camTransform.transform.position = cam.transform.position;
        }
        yChange.transform.LookAt(camTransformY.transform.position);
        xChange.transform.LookAt(camTransformX.transform.position);
        transform.localEulerAngles = new Vector3(transform.localPosition.x,xChange.transform.localEulerAngles.y, 
            transform.localPosition.z);
        //couldn't get zooming quite right
        //cam.transform.localPosition = (Vector3.back * -Input.GetAxis("Mouse ScrollWheel") * zoomSpeed) + cam.transform.localPosition;
        //cam.transform.Translate(cam.transform.TransformDirection(Vector3.forward) * Input.GetAxis("Mouse ScrollWheel") * zoomSpeed);
    }

    public void Enable(CastPlayer player)
    {
        
        on = !on;
        if (on)
        {
            camTransformX = player.camTransformX;
            camTransformY = player.camTransformY;
        }
        else
        {
            
        }
    }
    
}