//Kevin Friddle
//Created: 9/17/2016
//Last Updated: 9/19/2016
using UnityEngine;
using System.Collections;

public class DoorITBL : Interactable {

    public GameObject cam;
    public float speed;
    public Interact actor;  //object to act upon the door
    Rigidbody rb;
    bool rdSet; //has the ray distance been set

	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
        rdSet = false;
	}
	
	// Update is called once per frame
	void Update () {
	    if(interacting && !rdSet)
        {
            rdSet = true;
            actor.AnchorDistance = Vector3.Distance(cam.transform.position, transform.position);
        }
        else if(!interacting && rdSet)
        {
            rdSet = false;
        }
	}

    public override void Interact()
    {
        //rb.AddRelativeTorque(cam.transform.forward * speed);
        rb.angularVelocity += (actor.Anchor.GetPoint(actor.AnchorDistance) - transform.position) * speed * Time.deltaTime; 
    }
}
