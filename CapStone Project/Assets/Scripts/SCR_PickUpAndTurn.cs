using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_PickUpAndTurn : InteractableObject
{
    private Vector3 originPos;
    private Quaternion orgingRotation;
    private Vector3 newPos;
    private Quaternion newRotation;
    
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
            newRotation =  Quaternion.Euler(Input.GetAxisRaw("Horizontal")+ transform.rotation.eulerAngles.x,
                Input.GetAxisRaw("Vertical") + transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            Manager.instance.paused = true;
            Time.timeScale = 0;
        }
        else
        {
            Manager.instance.paused = false;
            Time.timeScale = 1;
        }

        if (Input.GetMouseButtonDown(0) && active)
        {
            Use(null);
        }
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
        }
    }
}
