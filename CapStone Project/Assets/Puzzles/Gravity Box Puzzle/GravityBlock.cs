using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBlock : MonoBehaviour
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
        
        if (!moveXAxis) transform.localPosition = new Vector3(originPos.x,transform.localPosition.y,transform.localPosition.z);
        if (!moveYAxis) transform.localPosition = new Vector3(transform.localPosition.x,originPos.y,transform.localPosition.z);
        
    }
}
