using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Magnets : InteractableObject
{
    public GameObject surface;
    public float initialxpos;
    public float offset = 2;
    public CastPlayer player;

    [SerializeField] private float currentrotation = 0;
    // Start is called before the first frame update
    void Start()
    {
        initialxpos = transform.position.x;
        currentrotation = UnityEngine.Random.Range(-15, 15);
        transform.GetChild(0).localRotation = Quaternion.Euler(0,-90,currentrotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            float length;
            float dotNumerator;
            float dotDenominator;
            Vector3 vector;
            Vector3 intersection = Vector3.zero;

            //calculate the distance between the linePoint and the line-plane intersection point
            dotNumerator = Vector3.Dot((Vector3.right*(initialxpos) - player.viewpoint.transform.position), Vector3.right);
            dotDenominator = Vector3.Dot(player.viewpoint.transform.forward, Vector3.right);

            length = dotNumerator / dotDenominator;

            vector = player.viewpoint.transform.forward*length;

            intersection = player.viewpoint.transform.position + vector;
            intersection = new Vector3(intersection.x, Mathf.Clamp(intersection.y, surface.GetComponent<Collider>().bounds.min.y, surface.GetComponent<Collider>().bounds.max.y), Mathf.Clamp(intersection.z, surface.GetComponent<Collider>().bounds.min.z, surface.GetComponent<Collider>().bounds.max.z)) ;
            transform.position = Vector3.Lerp(transform.position, intersection+Vector3.right*offset, 0.1f);
            if (currentrotation != 0)
                currentrotation = 0;
            transform.GetChild(0).localRotation = Quaternion.Slerp(transform.GetChild(0).localRotation,Quaternion.Euler(0,-90,0), 0.1f);
        }
        else
        {
            if (currentrotation == 0)
            {
                currentrotation = UnityEngine.Random.Range(-15, 15);
            }
            transform.position = Vector3.Lerp(transform.position, new Vector3(initialxpos, transform.position.y, transform.position.z),0.1f);
            transform.GetChild(0).localRotation = Quaternion.Slerp(transform.GetChild(0).localRotation,Quaternion.Euler(0,-90,currentrotation), 0.1f);
        }
        // possible alternative with raycasting place lifted pos to be along the line using reverse direction e.g -forward from view point
    }

    public override void Use(CastPlayer newPlayer)
    {
        player = newPlayer;
        active = !active;
    }
}
