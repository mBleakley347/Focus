using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastPlayer : MonoBehaviour
{
    public GameObject camTransformX;
    public GameObject camTransformY;
    
    public float height = 0.55f;
    public float radius = 0.2f;
    public float stepradius = 0.2f;
    public float movespeed = 2;

    public Vector3 gravitydirection = Vector3.down;
    

    [Header("Context State Machines")]
    public StateMachine<CastPlayer> playerContext;
    public StateBase<CastPlayer> OnGround;
    public StateBase<CastPlayer> InAir;
    
    /*[Header("Method State Machines")]
    public StateMachine<CastPlayer> playerMethod;

    public StateBase<CastPlayer> GroundClamp;
    public StateBase<CastPlayer> GroundMagnet;*/

    [Header("misc")]
    public Rigidbody body;

    public Camera viewpoint;

    public GameObject heldobject = null;
    
    [Header("View Bob")]
    private float bouncetime = 0f;
    public float bouncemax = 0.7f;
    public float bouncesize = 0.1f;
    private Vector3 startcampos = Vector3.zero;

    public bool walking = false;

    public SCR_PlayerController puzzleControl;
    // Start is called before the first frame update
    void Awake()
    {
        OnGround = GetComponent<GroundState>();
        InAir = GetComponent<AirState>();

        playerContext = new StateMachine<CastPlayer>();
        
        body = GetComponent<Rigidbody>();
        playerContext.ChangeState(OnGround,this);
        checkground(height,gravitydirection);
        startcampos = viewpoint.transform.localPosition;
    }

    public void MoveAxis(float a, float b)
    {
        body.velocity = Vector3.ProjectOnPlane(transform.right*a,gravitydirection)
                        +Vector3.ProjectOnPlane(transform.forward*b,gravitydirection)
                        +Vector3.Project(body.velocity,gravitydirection);

        walking = (body.velocity != Vector3.zero);
        if (bouncetime < bouncemax)
        {
            bouncetime += Time.deltaTime;
        }
        viewpoint.transform.localPosition =startcampos+ new Vector3(0, Bounce(bouncemax, bouncetime), 0);
        //float bounce = Mathf.Abs(Mathf.Sin(Time.time)) + 2f;
        //head.transform.localScale = new Vector3(bounce, bounce, bounce);
        //transform.GetChild(0).GetComponentInChildren<Transform>().localPosition = new Vector2(0, starting.y + ((Mathf.Sin(Time.time * 2) * 0.01f) - 0.01f));
    }
    public void XViewAxis(float i)
    {
        // left and right rotation
        transform.RotateAround(transform.position-transform.up*(height-radius),-gravitydirection,i);
    }
    public RaycastHit checkground(float Distance, Vector3 gravitydir,float clampdist =0)
    {
        
        float largerdist = Distance - radius;
        Vector3 expectedcenter = transform.position - (transform.up * largerdist)+(gravitydir.normalized*(radius-stepradius));
        Vector3 perpgravitydir = Vector3.ProjectOnPlane(expectedcenter-transform.position,transform.up);
        
        float smallerdist = stepradius < radius ? Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow(stepradius, 2)) : stepradius;
        smallerdist = largerdist+Mathf.Sqrt(Mathf.Pow(smallerdist,2)-Mathf.Pow(perpgravitydir.magnitude,2));

        
        RaycastHit hit;
        Physics.SphereCast(transform.position+perpgravitydir, stepradius, -transform.up, out hit, smallerdist);
        smallerhit = hit;
        
        if (smallerdist+stepradius > Distance &&Vector3.Distance(hit.point, transform.position -transform.up * largerdist) > radius+0.01f)
        {
            // the larger sphere cant contact this point, cast a bit shorter to make hit.collider null
            Physics.SphereCast(transform.position, stepradius, -transform.up, out hit, largerdist);
        }
        else
        {
            Debug.DrawLine(hit.point,hit.point+((transform.position -transform.up * largerdist)-hit.point).normalized*radius,Color.magenta);
            
            
            Debug.DrawLine(transform.position,(transform.position -transform.up * largerdist),Color.red);
            
            
            if (Vector3.Distance(hit.point, transform.position -transform.up * largerdist) <= radius + 0.01f)
            {
                if (clampdist > 0 && hit.point.y > transform.position.y - clampdist - radius)
                {
                    // need a fix here to stop player sliding down slope
                    
                    hit.RetargetRaycastToSphere(transform.position, -transform.up, radius, stepradius);
                }
                else
                {
                    // contact point is on the boundry intersection of the smaller sphere and larger one 
                    hit.normal = ((transform.position -transform.up * largerdist)-hit.point).normalized;
                    // if out feet are below the contact point, make sure we stand on the point
                    if (hit.point.y > transform.position.y - largerdist) // todo get y in terms of transform.up
                    {
                        Debug.Log("flipped");
                        hit.normal = new Vector3(hit.normal.x,-hit.normal.y,hit.normal.z);
                    }
                }
                // hit point is inside the normal range
                
                // since the contact point is on the boundary, we retarget the smaller sphere to the larger one by changing the normal direction
                
                Debug.DrawLine(hit.point,hit.point+hit.normal*radius,Color.magenta);
            }
            else
            {
                if (hit.point.y > transform.position.y - largerdist)
                {
                    Debug.Log("slipped");
                }
                // hit point is inside the normal range
                hit.RetargetRaycastToSphere(transform.position, gravitydir, radius, stepradius);
            }
        }

        largerhit = hit;
        return hit;
    }

    public RaycastHit smallerhit;
    public RaycastHit largerhit;

    
    
    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetButtonDown("Interact"))
        {
           
            if (!heldobject)
            {
                
                pickup();
            }
            else
            {
                drop();
            }
        }
        if (Manager.instance.paused) return;
        if (Manager.instance.puzzleOn) return;
        XViewAxis(Input.GetAxis("Mouse X")*3);
    }

    public void pickup()
    {
        camTransformX.transform.position = Camera.main.transform.position;
        camTransformY.transform.position = Camera.main.transform.position;
        RaycastHit hit;
        LayerMask objects = LayerMask.GetMask("Objects");
        if (Physics.Raycast(viewpoint.transform.position,viewpoint.transform.forward, out hit, 300,objects))
        {
            if (hit.transform.gameObject.GetComponent<SCR_BoxRotator>())
            {
                heldobject = hit.transform.root.gameObject;
                puzzleControl.enabled = false;
                heldobject.GetComponent<InteractableObject>().Use(this);
                return;
            }
            if (hit.transform.gameObject.GetComponent<InteractableObject>())
            {
                hit.transform.gameObject.GetComponent<InteractableObject>().Use(this);
            }
            heldobject = hit.transform.gameObject;

            //heldobject.angularDrag = 2;
                //heldobject.isKinematic = true;
                //heldobject.transform.SetParent(viewpoint.transform);
        }
    }

    public void drop()
    {
        heldobject.GetComponent<InteractableObject>().Use(null);
        //heldobject.transform.SetParent(null);
        //heldobject.isKinematic = false;
        //heldobject.angularDrag = 0.05f;
        heldobject = null;
    }

    private void FixedUpdate()
    {
    
        if (Manager.instance.paused) return;
        if (Manager.instance.puzzleOn) return;
        if (heldobject)
        {
            Vector3 dir = (viewpoint.transform.position + viewpoint.transform.forward) - heldobject.transform.position;
            //heldobject.velocity = dir * Mathf.Pow(dir.magnitude+2,2);
        }
        playerContext.RunState();
        MoveAxis(Input.GetAxis("Horizontal")*movespeed,Input.GetAxis("Vertical")*movespeed);
        Vector3 axis = Vector3.Cross(transform.up,-gravitydirection);
        float angle = Vector3.Angle(transform.up,-gravitydirection);
        transform.RotateAround(transform.position-transform.up*(height-radius),axis,angle*(0.1f+(Vector3.Dot(transform.up,-gravitydirection)-1)*0.9f/-2));
    }

    public float Bounce(float dur, float cur)
    {
        if (cur >= dur)
        {
            if (walking)
            {
                bouncetime = 0;
            }
            return 0;
        }
        else
        {
            return (-(bouncesize * ((cur * (cur - dur)) / (Mathf.Pow(dur / 2, 2f)))));
        }
    }
    private void OnDrawGizmos()
    {
        if (smallerhit.collider)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(smallerhit.point+(smallerhit.normal*stepradius),stepradius);
            
            if (largerhit.collider)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(largerhit.point+(largerhit.normal*radius),radius);
            }
            else
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position+(-transform.up*(height-radius)),radius);
            }
        }
        else
        {
            Vector3 perpgravitydir = Vector3.ProjectOnPlane(Vector3.down, transform.up).normalized;
            
            Vector3 expectedcenter = transform.position - (transform.up * (height-radius))+(Vector3.down*(radius-stepradius));
            perpgravitydir = Vector3.ProjectOnPlane(expectedcenter-transform.position,transform.up);
            
            float smallerdist = stepradius < radius ? Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow(stepradius, 2)) : stepradius;
            smallerdist = height - radius+ Mathf.Sqrt(Mathf.Pow(smallerdist,2)-Mathf.Pow(perpgravitydir.magnitude,2));
            
            
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position+(perpgravitydir)-transform.up*smallerdist,stepradius);
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position+(-transform.up*(height-radius)),radius);
        }
    }
}
