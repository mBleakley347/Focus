using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirState : StateBase<CastPlayer>
{
    public bool gravity = true;
    public bool juststarted = true;
    public override void Enter(CastPlayer Caller,StateMachine<CastPlayer> CallerMachine)
    {
        Debug.Log("AirState");
        juststarted = true;
        base.Enter(Caller,CallerMachine);
    }

    public override void Execute()
    {
        RaycastHit hit = owner.checkground(owner.height,Vector3.down);
        if (!hit.collider)
        {
            // if we are still in the air, keep falling
            if(gravity&&!juststarted)
                owner.body.AddForce(Physics.gravity.magnitude*owner.gravitydirection,ForceMode.Force);

            if(owner.gravitydirection!=Vector3.down)
                owner.gravitydirection = Vector3.down;
        }
        else
        {
            Debug.Log("Hit");
            // otherwise, stop falling
            if (!juststarted&&owner.body.velocity.y <= 0)
            {
                owner.body.velocity= new Vector3(owner.body.velocity.x,0,owner.body.velocity.z);
                ownerMachine.ChangeState(owner.OnGround,owner);
            }
        }

        if (juststarted)
            juststarted = false;

        
        base.Execute();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
