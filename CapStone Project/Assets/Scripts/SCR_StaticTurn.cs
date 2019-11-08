using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_StaticTurn : MonoBehaviour
{
    [SerializeField]private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
        float temp = Mathf.Repeat(transform.localEulerAngles.y + 180,360);
        transform.localRotation = Quaternion.Euler(0, Mathf.Clamp(temp, 140, 220), 0);
        
    }
}
