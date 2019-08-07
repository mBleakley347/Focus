using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GroundClampState : StateBase<CastPlayer>
{
    public float stepheight = 0.25f;
    [FormerlySerializedAs("groundclampdepth")] public float defaultgroundclampdepth = 0.25f;
    public float currentclampdepth = 0.25f;

    public override void Execute()
    {
        RaycastHit hit = owner.checkground(owner.height+currentclampdepth, -transform.up,owner.height);
        if (hit.collider)
        {
            // otherwise make sure to be the correct distance away
            transform.position = (hit.point + (hit.normal * owner.radius)) + (transform.up * (owner.height-owner.radius));
        }
        gravity(owner,hit);
    }

    public virtual void gravity(CastPlayer owner,RaycastHit hit)
    {
        if(owner.gravitydirection!=Vector3.down)
            owner.gravitydirection = Vector3.down;
    }
}
