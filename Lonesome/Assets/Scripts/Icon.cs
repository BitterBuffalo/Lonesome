//Kevin Friddle
//Created: 10/3/2016
//Last Updated: 10/3/2016
using UnityEngine;
using System.Collections;

public class Icon : MonoBehaviour {

    public Transform cam;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.rotation = Quaternion.LookRotation(transform.position - cam.position);
	}
}
