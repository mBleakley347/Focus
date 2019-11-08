using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_GravityBlock : MonoBehaviour
{
    public Vector3 originPos; 
    public bool moveXAxis;
    public bool moveYAxis;
    public bool masterBlock = false;
    // Start is called before the first frame update
    void Start()
    {
        originPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(transform.localPosition.x,transform.localPosition.y,originPos.z);
        if (!moveXAxis) transform.localPosition = new Vector3(originPos.x,transform.localPosition.y,originPos.z);
        if (!moveYAxis) transform.localPosition = new Vector3(transform.localPosition.x,originPos.y,originPos.z);
        
    }
}
