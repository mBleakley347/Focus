using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingBlock : PuzzleComponent
{
    /*
     * clamp possitions to dirrection of movement
     * slider/ distance checking to snaps to grid possition when released.
     */
    public Vector3 lastpostiton;
    private Rigidbody rb;
    public int force;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override bool Click()
    {
        lastpostiton = transform.position;
        return base.Click();
    }

    public override bool Hold(Vector3 a)
    {
        RaycastHit hit;
        Vector3 temp = lastpostiton+Vector3.Project(a-lastpostiton, transform.forward);
        /*
        Physics.BoxCast(lastpostiton, transform.localScale/2, Vector3.Project(a - lastpostiton,
            transform.forward), out hit, transform.rotation,Vector3.Project(a - lastpostiton,
            transform.forward).magnitude);
        rb.MovePosition(lastpostiton+(Vector3.Project(a - lastpostiton,
                                          transform.forward)*hit.distance));
        */
        //temp += new Vector3(transform.position.x, transform.position.y, 0);

        Vector3 temppos = rb.position;
        rb.position = lastpostiton;
        rb.SweepTest(Vector3.Project(a - lastpostiton, transform.forward).normalized, out hit);
        rb.position = temppos;
        
        float extent = Vector3.Project(transform.localScale / 2, Vector3.Project(a - lastpostiton,
            transform.forward).normalized).magnitude;
        if (Vector3.Project(hit.point-transform.position, transform.forward).magnitude+(extent) < Vector3.Project(a - lastpostiton, transform.forward).magnitude&&hit.collider)
        {
            Debug.Log("hitting, extent = "+extent);
            rb.MovePosition(transform.position+transform.forward*(Vector3.Project(hit.point-transform.position, transform.forward).magnitude-extent));
        }
        else
        {
            rb.MovePosition(temp);
        }
        
        return base.Hold(a);
        
    }
}
