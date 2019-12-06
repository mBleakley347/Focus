using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour {

    public float maxdown = 45;
    public float maxup = -45;
    public bool yinv,ylock = false;
    public float mouseSensitivity = 100.0f;
    [SerializeField] private CastPlayer player;
	// Update is called once per frame
	void LateUpdate ()
    {
	    if (Manager.instance.puzzleOn || Manager.instance.menuUp || Manager.instance.paused) return;
        if (Manager.instance.finished)
        {
            Vector3 temp = player.target.transform.position - transform.position;
            Quaternion temp2 = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(temp, transform.up), 0.1f);
            transform.localRotation = Quaternion.Euler(temp2.eulerAngles.x, 0, 0);
            return;
        }
		    float tempy = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
			tempy = Convert.ToBoolean(PlayerPrefs.GetInt("InvertYAxis",1))?tempy:-tempy;
		    Rotate(transform,tempy,new Vector2(maxdown,maxup), ylock);
	}
	
	public float vertical = 0;
	public void Rotate(Transform Object, float y, Vector2 yAxisMaxs,bool ylock)
	{
		if (!ylock)
		{
			//vertical = (vertical+y).CircleClamp(0,45); // x is up and y is left/right unfortunately
			if (yAxisMaxs.x!=yAxisMaxs.y)
			{
				vertical = Mathf.Clamp(vertical+y,yAxisMaxs.x,yAxisMaxs.y);
			}
			else
			{
				Debug.Log("shouldnt "+yAxisMaxs.x+" "+yAxisMaxs.y);
				vertical += y;
			}
		}
		Object.localRotation = Quaternion.Euler(vertical,0,0);
	}
}
