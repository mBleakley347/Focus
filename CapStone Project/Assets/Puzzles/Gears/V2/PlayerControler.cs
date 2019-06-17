using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{

    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100)){
                Interactable pressable = hit.collider.GetComponent<Interactable>();
                if (pressable != null) {
                    pressable.Click();
                }
            }
        }
    }
}
