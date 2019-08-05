using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGoal : PuzzleComponent
{
    private GravityBlock _gravityBlock;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (complete)
        {
            if (_gravityBlock.transform.localPosition.x <= transform.localPosition.x - 0.01f || _gravityBlock.transform.localPosition.x >= transform.localPosition.x + 0.01f ||
                _gravityBlock.transform.localPosition.y <= transform.localPosition.y - 0.01f || _gravityBlock.transform.localPosition.y >= transform.localPosition.y + 0.01f)
            {
                
                _gravityBlock.transform.localPosition = Vector3.Lerp(_gravityBlock.transform.localPosition,
                    new Vector3(transform.localPosition.x, transform.localPosition.y, _gravityBlock.transform.localPosition.z), 0.05f);
                _gravityBlock.originPos = _gravityBlock.transform.localPosition;
            }
            else if (_gravityBlock.transform.localPosition.z <= transform.localPosition.z - 0.5f || _gravityBlock.transform.localPosition.z >= transform.localPosition.z + 0.5f )
            {
                
                _gravityBlock.transform.localPosition = Vector3.Lerp(_gravityBlock.transform.localPosition, new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z), 0.01f);
                _gravityBlock.originPos = _gravityBlock.transform.localPosition;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other?.gameObject?.GetComponent<GravityBlock>().masterBlock == true)
        {
            other.gameObject.GetComponent<Rigidbody>().useGravity = false;
            _gravityBlock = other.gameObject.GetComponent<GravityBlock>();

            _gravityBlock.originPos = _gravityBlock.transform.localPosition;
            _gravityBlock.moveXAxis = false;
            _gravityBlock.moveYAxis = false;
            complete = true;
        }
    }
}
