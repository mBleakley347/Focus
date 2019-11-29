using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Ball : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rb;
    private Vector3 origin;
    public bool released;
    // Start is called before the first frame update
    void Start()
    {
        SCR_ResetArea.ResetPuzzle += Reset;
        origin = transform.localPosition;
    }

    public void Reset()
    {
        released = false;
        rb.velocity.Set(0,0,0);
        rb.angularVelocity.Set(0,0,0);
        rb.Sleep();
        transform.localPosition = origin;
    }

    public void End()
    {
        Destroy(this);
        released = false;
        rb.velocity.Set(0,0,0);
        rb.angularVelocity.Set(0,0,0);
        rb.Sleep();
    }
    // Update is called once per frame
    void Update()
    {
        if (released)
        {
            rb.AddForce(-transform.up * speed);
        } else
        {
            transform.localPosition = origin;
        }
    }
}
