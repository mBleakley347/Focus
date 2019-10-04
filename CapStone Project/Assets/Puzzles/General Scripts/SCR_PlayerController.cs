using UnityEngine;

public class SCR_PlayerController : MonoBehaviour
{
    public Camera cam;

    public Vector3 offset;

    private SCR_Interactable pressable;
    private Vector3 screentoworld;

    private float timer;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            castRay();
            if (pressable != null)
            {
                offset = pressable.transform.position -
                         cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                             screentoworld.z));
                pressable.Click(offset);
            }
        }

        if (Input.GetMouseButton(0))
            if (pressable != null)
            {
                var cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screentoworld.z);
                var cursorPosition = cam.ScreenToWorldPoint(cursorPoint) + offset;
                pressable.Hold(cursorPosition);
            }

        if (Input.GetMouseButtonUp(0))
        {
            if (pressable != null)
            {
                pressable.Release();
            }
            pressable = null;
        }
    }

    public void castRay()
    {
        var ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            screentoworld = cam.WorldToScreenPoint(hit.collider.transform.position);
            pressable = hit.collider.GetComponent<SCR_Interactable>();
        }
    }

    public void ForceMouseUp(float time)
    {
        timer = time;
        pressable = null;
    }
    
}