using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_PickUpAndTurn : InteractableObject
{
    
    public Vector3 originPos;
    public  Quaternion orgingRotation;
    public  Vector3 newPos;
    public  Quaternion newRotation;
    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        originPos = transform.position;
        orgingRotation = transform.rotation;
        newPos = originPos;
        newRotation = orgingRotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, newPos, 0.05f);
        transform.rotation = Quaternion.Lerp(transform.rotation,newRotation,0.05f);
        if (active)
        {
            if (Input.GetMouseButton(1))
            {
                newRotation = Quaternion.Euler(Input.GetAxisRaw("Mouse Y") * speed + transform.eulerAngles.x,
                Input.GetAxisRaw("Mouse X") * speed + transform.eulerAngles.y, transform.eulerAngles.z);
            }
            Manager.instance.paused = true;
            Time.timeScale = 0;
        }
        // was randomly double clicking
        /*if (Input.GetMouseButtonDown(0) && active)
        {
            Use(null);
        }*/
    }

    public override void Use(CastPlayer player)
    {
        
        active = !active;
        if (active)
        {
            newPos = (player.viewpoint.transform.position + player.viewpoint.transform.forward);
        }
        else
        {
            newPos = originPos;
            newRotation = orgingRotation;
            Manager.instance.paused = false;
            Time.timeScale = 1;
        }
    }
}
