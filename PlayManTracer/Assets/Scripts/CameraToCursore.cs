using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraToCursore : MonoBehaviour {

    public float Hspeed = 2f;
    public float Vspeed = 2f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float h = Hspeed * Input.GetAxis("Mouse X");
        float v = Vspeed * Input.GetAxis("Mouse Y");
        transform.Rotate(-v, h, 0);
    }
}
