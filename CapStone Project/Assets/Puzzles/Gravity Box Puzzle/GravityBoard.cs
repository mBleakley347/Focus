using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBoard : Interactable
{
    [SerializeField] private float rotationSpeed;

    private Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool Click(Vector3 a)
    {
        pos = a;
        return base.Click(a);
    }

    public override bool Hold(Vector3 a)
    {
        if (pos.y >= 0)
        {
            transform.Rotate(0, 0, Input.GetAxis("Mouse X") * rotationSpeed);
        }
        else
        {
            transform.Rotate(0, 0, -Input.GetAxis("Mouse X") * rotationSpeed);
        }

        return base.Hold(a);
    }
}
