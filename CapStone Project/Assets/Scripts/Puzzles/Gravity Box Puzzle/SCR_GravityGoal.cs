using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_GravityGoal : SCR_PuzzleComponent
{
    private SCR_GravityBlock _scrGravityBlock;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (complete)
        {
            if (_scrGravityBlock.transform.localPosition.x <= transform.localPosition.x - 0.01f || _scrGravityBlock.transform.localPosition.x >= transform.localPosition.x + 0.01f ||
                _scrGravityBlock.transform.localPosition.y <= transform.localPosition.y - 0.01f || _scrGravityBlock.transform.localPosition.y >= transform.localPosition.y + 0.01f)
            {
                
                _scrGravityBlock.transform.localPosition = Vector3.Lerp(_scrGravityBlock.transform.localPosition,
                    new Vector3(transform.localPosition.x, transform.localPosition.y, _scrGravityBlock.transform.localPosition.z), 0.05f);
                _scrGravityBlock.originPos = _scrGravityBlock.transform.localPosition;
            }
            else if (_scrGravityBlock.transform.localPosition.z <= transform.localPosition.z - 0.5f || _scrGravityBlock.transform.localPosition.z >= transform.localPosition.z + 0.5f )
            {
                
                _scrGravityBlock.transform.localPosition = Vector3.Lerp(_scrGravityBlock.transform.localPosition, new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z), 0.01f);
                _scrGravityBlock.originPos = _scrGravityBlock.transform.localPosition;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other?.gameObject?.GetComponent<SCR_GravityBlock>()?.masterBlock == true)
        {
            other.gameObject.GetComponent<Rigidbody>().useGravity = false;
            _scrGravityBlock = other.gameObject.GetComponent<SCR_GravityBlock>();
            _scrGravityBlock.enabled = false;
            /*_scrGravityBlock.originPos = _scrGravityBlock.transform.localPosition;
            _scrGravityBlock.moveXAxis = false;
            _scrGravityBlock.moveYAxis = false;*/
            complete = true;
        }
    }
}
