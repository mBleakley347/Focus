using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_BoxRotator : MonoBehaviour
{
    public Camera cam;
    public GameObject camTransformX;
    public GameObject camTransformY;
    public GameObject yChange;
    public GameObject xChange;
    public float zoomSpeed;

    public float turnSpeed;
    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        camTransformY.transform.position = cam.transform.position;
        camTransformX.transform.position = cam.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Manager.instance.paused) return;
        //rotation
        if (Input.GetMouseButton(1))
        {
            
            //camTransformY.transform.Translate(transform.up * -Input.GetAxis("Mouse Y") * turnSpeed);
            //camTransformX.transform.Translate(transform.right * -Input.GetAxis("Mouse X") * turnSpeed);
            //camTransformY.transform.LookAt(this.transform.position);
            //camTransformX.transform.LookAt(this);
            if (camTransformY.transform.position.z >= 0)
            {
                camTransformY.transform.RotateAround(transform.position,camTransformY.transform.right,-Input.GetAxis("Mouse Y") * turnSpeed);
            }
            else
            {
                camTransformY.transform.RotateAround(transform.position,-camTransformY.transform.right,-Input.GetAxis("Mouse Y") * turnSpeed);
            }

            if (camTransformY.transform.position.z >= 1)
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
            //cam.transform.RotateAround(this.transform.position, Vector3.up, -Input.GetAxis("Mouse X") * turnSpeed);
            //cam.transform.RotateAround(this.transform.position, Vector3.right, Input.GetAxis("Mouse Y") * turnSpeed);
            yChange.transform.LookAt(camTransformY.transform.position);
            xChange.transform.LookAt(camTransformX.transform.position);
            transform.localEulerAngles = new Vector3(transform.localPosition.x,xChange.transform.localEulerAngles.y, 
                transform.localPosition.z);
            //transform.LookAt(new Vector3(camTransformX.transform.position.x,camTransformY.transform.position.y,camTransformX.transform.position.z));
                                                
            cam.transform.LookAt(this.transform.position);
        }

        if (Input.GetMouseButtonUp(1))
        {
        }
        if (Input.GetMouseButtonDown(1))
        {
            //camTransform.transform.position = cam.transform.position;
        }
        //couldn't get zooming quite right
        //cam.transform.localPosition = (Vector3.back * -Input.GetAxis("Mouse ScrollWheel") * zoomSpeed) + cam.transform.localPosition;
        //cam.transform.Translate(cam.transform.TransformDirection(Vector3.forward) * Input.GetAxis("Mouse ScrollWheel") * zoomSpeed);
    }
}