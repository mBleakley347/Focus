using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapSphereCollider : MonoBehaviour
{
    public Vector3 relativePos = Vector3.zero;
    public float radius = 0.5f;
    public Rigidbody body;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        checkCollision();
    }

    //todo move both into class addons for re-implimented colliders
    public bool checkCollision()
    { 
        bool contact = false;
 
        foreach (Collider col in Physics.OverlapSphere(transform.TransformPoint(relativePos), radius))
        {
            Vector3 contactPoint = Vector3.zero;// = col.ClosestPointOnBounds(transform.position);
            if (col is BoxCollider)
            {
                contactPoint = col.ClosestPointOnBounds(transform.TransformPoint(relativePos));
            }
            else if (col is SphereCollider)
            {
                contactPoint = ClosestPointOn((SphereCollider)col, transform.TransformPoint(relativePos));
            }
            Vector3 v = transform.TransformPoint(relativePos) - contactPoint;
 
            transform.position += Vector3.ClampMagnitude(v, Mathf.Clamp(radius - v.magnitude, 0, radius));

            // make sure out velocity can't squish us into a wall
            if (col is BoxCollider)
            {
                if (Vector3.Dot(Vector3.Project(body.velocity, v), v - transform.TransformPoint(relativePos)) < 0)
                {
                    body.velocity = Vector3.ProjectOnPlane(body.velocity, v.normalized);
                    //Debug.Log("pushback "+body.velocity+" vector "+v);
                }
            }
            else if (col is SphereCollider) //todo figure out velocity clamping case for spheres
            {
                if (Vector3.Dot(Vector3.Project(body.velocity, v), v - transform.TransformPoint(relativePos)) < 0)
                {
                    body.velocity = Vector3.ProjectOnPlane(body.velocity, v.normalized);
                    //Debug.Log("pushback "+body.velocity+" vector "+v);
                }
            }
            
 
            contact = true;
        }
        return contact;
    }
    Vector3 ClosestPointOn(SphereCollider collider, Vector3 to)
    {
        Vector3 p;
 
        p = to - collider.transform.position;
        p.Normalize();
 
        p *= collider.radius * collider.transform.localScale.x;
        p += collider.transform.position;
 
        return p;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.TransformPoint(relativePos),radius);
    }
}
