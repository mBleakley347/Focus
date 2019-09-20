using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_BoxRotator : MonoBehaviour
{
    public Camera cam;

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
        //roation
        if (Input.GetMouseButton(1))
        {
            cam.transform.Translate(transform.up * -Input.GetAxis("Mouse Y") * turnSpeed);
            cam.transform.Translate(transform.right * Input.GetAxis("Mouse X") * turnSpeed);
            if (Vector3.Distance(cam.transform.position, transform.position) > distance)
            {
                cam.transform.Translate(cam.transform.InverseTransformDirection(cam.transform.forward)* zoomSpeed);
            }
            else if (Vector3.Distance(cam.transform.position, transform.position) < distance)
            {
                cam.transform.Translate(-cam.transform.InverseTransformDirection(cam.transform.forward) * zoomSpeed);
            }
            //cam.transform.RotateAround(this.transform.position, Vector3.up, -Input.GetAxis("Mouse X") * turnSpeed);
            //cam.transform.RotateAround(this.transform.position, Vector3.right, Input.GetAxis("Mouse Y") * turnSpeed);
            cam.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
            cam.transform.LookAt(this.transform.position);
        }
        //couldnt get zooming quite right
        //cam.transform.localPosition = (Vector3.back * -Input.GetAxis("Mouse ScrollWheel") * zoomSpeed) + cam.transform.localPosition;
        //cam.transform.Translate(cam.transform.TransformDirection(Vector3.forward) * Input.GetAxis("Mouse ScrollWheel") * zoomSpeed);
    }
}