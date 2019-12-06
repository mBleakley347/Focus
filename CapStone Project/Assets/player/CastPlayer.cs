﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
///using UnityEngine.WSA;
using Random = System.Random;

public class CastPlayer : MonoBehaviour
{
    public GameObject camTransformX;
    public GameObject camTransformY;
    
    public float height = 0.55f;
    public float radius = 0.2f;
    public float stepradius = 0.2f;
    public float movespeed = 2;
    public float scale = 1;

    public Vector3 gravitydirection = Vector3.down;
    
    public Animator animator;

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
    [FormerlySerializedAs("bouncesize")] public float vertbouncesize = 0.1f;
    public float horizontalbouncesize = 0.1f;
    private Vector3 startcampos = Vector3.zero;
    public List<AudioClip> DefaultSounds;
    public List<AudioClip> TileSounds;
    public int numcolliders = 0;

    public bool walking = false;
    public bool rbHeld = false;

    public SCR_PlayerController puzzleControl;
    public float distance;

    public GameObject target;

    // Start is called before the first frame update
    void Awake()
    {
        OnGround = GetComponent<GroundState>();
        InAir = GetComponent<AirState>();

        playerContext = new StateMachine<CastPlayer>();
        
        body = GetComponent<Rigidbody>();
        playerContext.ChangeState(OnGround,this);
        checkground(height,gravitydirection);
        startcampos = viewpoint.transform.parent.localPosition;
    }

    public void MoveAxis(float a, float b)
    {
        body.velocity = Vector3.ProjectOnPlane(transform.right*a,gravitydirection)
                        +Vector3.ProjectOnPlane(transform.forward*b,gravitydirection)
                        +Vector3.Project(body.velocity,gravitydirection);

        Vector3 temp = scale * startcampos;
        walking = (Mathf.Abs(a) >= 0.1f|| Mathf.Abs(b)>=0.1f);
        if (bouncetime < bouncemax)
        {
            bouncetime += Time.deltaTime;
        }
        float bounceval = Bounce(bouncemax, bouncetime);
        
        if (Convert.ToBoolean(PlayerPrefs.GetInt("Viewbob",1)))
        {
            temp += body.velocity.magnitude/2 * new Vector3(scale*(horizontalbouncesize*Mathf.Sin((bouncetime*2*Mathf.PI)/bouncemax)), scale*(vertbouncesize*bounceval), 0);
        }

        //Debug.Log(temp); // trying to figure out the sliding and late bouncing

        viewpoint.transform.parent.localPosition = temp;
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
        if (Manager.instance.menuUp)
        {
            return;
        }else
        {
            animator.SetBool("Move", true);
        }
        if (Manager.instance.finished)
        {
            Vector3 temp = target.transform.position - transform.position;
            Quaternion temp2 = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(temp, transform.up), 0.1f);
            transform.localRotation = Quaternion.Euler( 0, temp2.eulerAngles.y, 0);
            return;
        }
        checkInteracton();
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
        if (heldobject)
        {
            if (Vector3.Distance(this.transform.position, heldobject.transform.position) > distance) drop();
        }
        if (Manager.instance.paused || Manager.instance.puzzleOn)
        {
            //body.velocity.Set(0,0,0);
            return;
        }
        XViewAxis(Input.GetAxis("Mouse X")*3);
    }

    public void checkInteracton()
    {
        RaycastHit hit;
        LayerMask objects = LayerMask.GetMask("Objects");
        if (Physics.SphereCast(viewpoint.transform.position, 0.2f, viewpoint.transform.forward, out hit, 5, objects))
        {
                Manager.instance.focusText.active = true;                
        }
        else
        {
            Manager.instance.focusText.active = false;
        }
    }
    public void pickup()
    {
        camTransformX.transform.position = Camera.main.transform.position;
        camTransformY.transform.position = Camera.main.transform.position;
        RaycastHit hit;
        LayerMask objects = LayerMask.GetMask("Objects");
        if (Physics.SphereCast(viewpoint.transform.position, 0.2f,viewpoint.transform.forward, out hit, 5,objects))
        {
            if (hit.transform.gameObject.GetComponent<SCR_BoxRotator>())
            {
                heldobject = hit.transform.root.gameObject;
                puzzleControl.enabled = false;
                heldobject.GetComponent<InteractableObject>().Use(this);
                return;
            } else if (hit.transform.gameObject.GetComponent<InteractableObject>())
            {
                hit.transform.gameObject.GetComponent<InteractableObject>().Use(this);
            } else if (hit.transform.gameObject.GetComponent<Rigidbody>())
            {
                rbHeld = true;
                hit.transform.gameObject.GetComponent<Rigidbody>().angularDrag = 2;
            }
            heldobject = hit.transform.gameObject;

            //heldobject.angularDrag = 2;
                //heldobject.isKinematic = true;
                //heldobject.transform.SetParent(viewpoint.transform);
        }
    }

    public void drop()
    {

        //heldobject.transform.SetParent(null);
        //heldobject.isKinematic = false;
        
        if (rbHeld)
        {
            rbHeld = false;
            heldobject.GetComponent<Rigidbody>().angularDrag = 0.05f;
        }
        else
        {
            heldobject.GetComponent<InteractableObject>().Use(null);
        }
        heldobject = null;

    }

    private void FixedUpdate()
    {

        if (Manager.instance.paused || Manager.instance.puzzleOn || Manager.instance.menuUp)
        {
            body.velocity = Vector3.zero;
            return;
        }
        if (heldobject)
        {
            if (rbHeld)
            {
                Vector3 dir = (viewpoint.transform.position + viewpoint.transform.forward) - heldobject.transform.position;
                heldobject.GetComponent<Rigidbody>().velocity = dir * Mathf.Pow(dir.magnitude+2,2);
            }
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
                if (numcolliders >= 1)
                {
                    if (TileSounds.Count > 0)
                    {
                        SCR_AudioManager.instanceAM.footstepSouce?.PlayOneShot(
                            TileSounds[UnityEngine.Random.Range(0,TileSounds.Count-1)]);
                    }
                }
                else
                {
                    if (DefaultSounds.Count > 0)
                    {
                        SCR_AudioManager.instanceAM.footstepSouce?.PlayOneShot(
                            DefaultSounds[UnityEngine.Random.Range(0, DefaultSounds.Count - 1)]);
                    }
                }
            }
            return 0;
        }
        else
        {
            return (-((cur * (cur - dur)) / (Mathf.Pow(dur / 2, 2f))));
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
