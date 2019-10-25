using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_BoxRotator : MonoBehaviour
{
    public Camera cam;
    public GameObject camTransform;
    public float zoomSpeed;

    public float turnSpeed;
    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Manager.instance.paused) return;
        //rotation
        if (Input.GetMouseButton(1))
        {
            camTransform.transform.Translate(transform.up * -Input.GetAxis("Mouse Y") * turnSpeed);
            camTransform.transform.Translate(transform.right * -Input.GetAxis("Mouse X") * turnSpeed);
            
            if (Vector3.Distance(camTransform.transform.position, transform.position) > distance + 1)
            {
                camTransform.transform.position = Vector3.Lerp(camTransform.transform.position, this.transform.position, zoomSpeed);
            }
            //cam.transform.RotateAround(this.transform.position, Vector3.up, -Input.GetAxis("Mouse X") * turnSpeed);
            //cam.transform.RotateAround(this.transform.position, Vector3.right, Input.GetAxis("Mouse Y") * turnSpeed);
            this.transform.LookAt(camTransform.transform.position);
            cam.transform.LookAt(this.transform.position);
        }

        if (Input.GetMouseButtonUp(1))
        {
        }
        if (Input.GetMouseButtonDown(1))
        {
            camTransform.transform.position = cam.transform.position;
        }
        //couldn't get zooming quite right
        //cam.transform.localPosition = (Vector3.back * -Input.GetAxis("Mouse ScrollWheel") * zoomSpeed) + cam.transform.localPosition;
        //cam.transform.Translate(cam.transform.TransformDirection(Vector3.forward) * Input.GetAxis("Mouse ScrollWheel") * zoomSpeed);
    }
}