using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_PhotoChange : SCR_PickUpAndTurn
{

    public GameObject[] picture;


    public Material[] images;
    private int currentImage;
    private int currentPicture;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, newPos, 0.05f);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, 0.05f);
        if (active)
        {
            newRotation = Quaternion.Euler(Input.GetAxisRaw("Horizontal") + transform.eulerAngles.x,
                Input.GetAxisRaw("Vertical") + transform.eulerAngles.y, transform.eulerAngles.z);
            //Manager.instance.paused = true;
            Time.timeScale = 0;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            // do we want different objects or just two images changing places repeatedly 
            // movement going up and rotating 90 degress just off the right side
            // before reversing below the second picture before changing to the next picture
            Switch();
        }
    }

    private void Switch()
    {
        currentImage++;
        if (currentImage == images.Length) currentImage = 0;

    }
}
