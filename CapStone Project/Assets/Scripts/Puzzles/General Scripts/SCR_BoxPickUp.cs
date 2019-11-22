using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_BoxPickUp : SCR_PickUpAndTurn
{
    
    public Camera cam;
    public SCR_BoxRotator rotation;

   
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, newPos, 0.05f);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, 0.05f);
        if (active)
        {
            //newRotation = Quaternion.Euler(Input.GetAxisRaw("Horizontal") * speed + transform.eulerAngles.x ,
                //Input.GetAxisRaw("Vertical") * speed + transform.eulerAngles.y , transform.eulerAngles.z);
            //Manager.instance.paused = true;
            Manager.instance.puzzleOn = true;
            Time.timeScale = 0;
        }
    }

    public override void Use(CastPlayer player)
    {
        active = !active;
        if (active)
        {
            newPos = (player.viewpoint.transform.position + player.viewpoint.transform.forward/5);
            player.camTransformX.transform.position = new Vector3(player.camTransformX.transform.position.x,newPos.y,player.camTransformX.transform.position.z);
            rotation.Enable(player);
            player.puzzleControl.enabled = true;
            //Manager.instance.paused = true;
            Manager.instance.ChangeCursorMode(false);
        }
        else
        {
            
            Manager.instance.ChangeCursorMode(true);
            //Manager.instance.paused = false;
            Manager.instance.puzzleOn = false;
            Time.timeScale = 1;
            newPos = originPos;
            newRotation = orgingRotation;
            rotation.Enable(player);          
            
        }
    }
}
