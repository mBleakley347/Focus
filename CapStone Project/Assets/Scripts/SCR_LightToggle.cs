using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_LightToggle : InteractableObject
{
    public Light light;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Use(CastPlayer player)
    {
        light.enabled = !light.enabled; 
    }
}
