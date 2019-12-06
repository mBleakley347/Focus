using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_PhotoChange : SCR_PickUpAndTurn
{

    public GameObject[] picture;
    public Material[] images;
    public Vector3 offSet;
    private int currentImage;
    private int currentPicture;
    private float delay;
    public Vector3 nextpos;
    private bool start = false;
    // Start is called before the first frame update
    void Start()
    {
        originPos = transform.position;
        orgingRotation = transform.rotation;
        newPos = originPos;
        newRotation = orgingRotation;
        for (int i = 0; i < picture.Length; i++)
        {
            picture[i].GetComponent<MeshRenderer>().material = images[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, newPos, 0.05f);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, 0.05f);
        if (start)
        {
            for (int i = 0; i < picture.Length; i++)
            {
                if (i == currentPicture)
                {
                    picture[i].transform.localPosition = Vector3.Lerp(picture[i].transform.localPosition, Vector3.zero, 0.1f);
                    
                }
                else
                {
                    picture[i].transform.localPosition = Vector3.Lerp(picture[i].transform.localPosition, new Vector3(0, -0.01f, 0), 0.1f);
                }
                picture[i].transform.rotation = Quaternion.Lerp(picture[i].transform.rotation,Quaternion.Euler(0,0,0),0.1f);
            }
            return;
        }
        else if (active)
        {
            int temp = Mathf.RoundToInt(Mathf.Repeat(currentPicture - 1, picture.Length));
            for (int i = 0; i < picture.Length; i++)
            {
                if (i == temp)
                {
                    picture[temp].transform.localPosition = Vector3.Lerp(picture[temp].transform.localPosition, nextpos, .1f);
                }
                else if (i == currentPicture)
                {
                    picture[currentPicture].transform.localPosition = Vector3.Lerp(picture[currentPicture].transform.localPosition, Vector3.zero, 0.1f);
                }
                else
                {
                    picture[i].transform.localPosition = Vector3.Lerp(picture[i].transform.localPosition, new Vector3(0, -0.01f, 0), 0.1f);
                }
            }
            
        }
        if (active)
        {
            if (Input.GetMouseButton(1))
            {
                newRotation = Quaternion.Euler(Input.GetAxisRaw("Mouse Y") * speed + transform.eulerAngles.x,
                Input.GetAxisRaw("Mouse X") * speed + transform.eulerAngles.y, transform.eulerAngles.z);
            }
            
            Manager.instance.paused = true;
            //Time.timeScale = 0;
            if (Input.GetMouseButton(0) && delay < 0)
            {

                // do we want different objects or just two images changing places repeatedly 
                // movement going up and rotating 90 degress just off the right side
                // before reversing below the second picture before changing to the next picture 
                Switch();
                StartCoroutine(Move());
            }
        } else
        {
            StopAllCoroutines();
            nextpos = new Vector3(0, -0.01f, 0);
        }
        
        

        delay -= Time.deltaTime;
    }

    private void Switch()
    {
        StopCoroutine(Move());
        delay = 2;
        currentImage++;
        if (currentImage == images.Length) currentImage = 0;
        currentPicture++;
        if (currentPicture == picture.Length) currentPicture = 0;

        picture[currentPicture].GetComponent<MeshRenderer>().material = images[currentImage];

    }
    IEnumerator Move()
    {
        nextpos = offSet;
        yield return new WaitForSeconds(1);
        nextpos = new Vector3(0, -0.01f, 0);
    }
    IEnumerator SetUp()
    {
        start = true;
        yield return new WaitForSeconds(2);
        start = false;

    }
    public override void Use(CastPlayer player)
    {

        active = !active;
        if (active)
        {
            StartCoroutine(SetUp());
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
