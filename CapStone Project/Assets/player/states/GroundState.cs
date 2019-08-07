using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundState : StateBase<CastPlayer>
{
    public float stepheight = 0.25f;
    public float groundclampdepth = 0.25f;

    public override void Enter(CastPlayer Caller,StateMachine<CastPlayer> CallerMachine)
    {
        Debug.Log("GroundState");
        base.Enter(Caller,CallerMachine);
        //owner.playerMethod.ChangeState(owner.GroundClamp,owner);
        owner.body.velocity = Vector3.zero;//new Vector3(owner.body.velocity.x,0,owner.body.velocity.z);
    }

    public override void Execute()
    {
        //float veltime = owner.body.velocity.magnitude > 20 ? 1 : owner.body.velocity.magnitude / 20;
        RaycastHit hit = owner.checkground(owner.height+groundclampdepth, -transform.up,owner.height);
        if (hit.collider)
        {
            //Debug.DrawLine(hit.point,hit.point + hit.normal * (owner.radius),Color.red);
            //Debug.DrawLine(hit.point + hit.normal * (owner.radius),(hit.point + hit.normal * (owner.radius))+(transform.up * (owner.height-owner.radius)),Color.yellow);

            // otherwise make sure to be the correct distance away
            transform.position = (hit.point + (hit.normal * owner.radius)) +
                                 (transform.up * (owner.height - owner.radius));

            
            if (owner.gravitydirection != Vector3.down)
                owner.gravitydirection = Vector3.down;
        }
        else
        {
            ownerMachine.ChangeState(owner.InAir,owner);
        }

        // otherwise make sure y velocity is 0 (need to change later for platforms)
        //owner.body.velocity = new Vector3(owner.body.velocity.x,0,owner.body.velocity.z);
        base.Execute();
    }
}
