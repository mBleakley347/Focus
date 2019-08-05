using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBoard : Interactable
{
    [SerializeField] private float rotationSpeed;
    public Vector3 newRotation;

    private Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.rotation.z < newRotation.z - 1 || transform.rotation.z > newRotation.z + 1)
        {
            transform.Rotate(Vector3.Lerp(transform.rotation.eulerAngles, newRotation, rotationSpeed));
        }
    }

    public override bool Click(Vector3 a)
    {
        pos = a;
        if (pos.x >= 0)
        {
            newRotation = new Vector3(newRotation.x,newRotation.y, newRotation.z +90);
        }
        else
        {
            newRotation = new Vector3(newRotation.x,newRotation.y, newRotation.z -90);
        }
        return base.Click(a);
    }

    public override bool Hold(Vector3 a)
    {
        /*
        a -= pos;
        Vector3 FacePos = Vector3.ProjectOnPlane(transform.InverseTransformPoint(a), transform.forward);
        //FacePos.Normalize();
        //currently looking from higher then center and is errattic
        //transform.LookAt(transform.forward,transform.TransformPoint(FacePos));
        Quaternion direction = Quaternion.LookRotation(transform.forward, transform.TransformPoint(FacePos));
        transform.rotation = Quaternion.Lerp(transform.rotation, direction,rotationSpeed);
        /*pos = a;
        if (pos.y >= 0)
        {
            transform.Rotate(0, 0, Input.GetAxis("Mouse X") * rotationSpeed);
        }
        else
        {
            transform.Rotate(0, 0, -Input.GetAxis("Mouse X") * rotationSpeed);
        }

        if (pos.x >= 0)
        {
            transform.Rotate(0,0,Input.GetAxis("Mouse Y") * rotationSpeed);
        }
        else
        {
            transform.Rotate(0,0,-Input.GetAxis("Mouse Y") * rotationSpeed);
        }
*/

        return base.Hold(a);
    }
}
