//Kevin Friddle
//Created: 9/17/2016
//Last Updated: 10/3/2016
using UnityEngine;
using System.Collections;

public class DoorITBL : Interactable {
    
    public float speed; //the speed at which the door opens
    public float rotationOffset;    //while interacted, this is the minimum rotation offset at which the door will begin to move
    public Rigidbody doorknob;
    public Rigidbody[] otherRbs;    //all rigibodies that the doorknob acts upon

    GameObject initialCamRotation;

	// Use this for initialization
	public override void Start () {
        base.Start();
        initialCamRotation = null;

        doorknob.maxAngularVelocity = 100;

        foreach(Rigidbody rb in otherRbs)
        { rb.maxAngularVelocity = 100; }
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Interacted = false;
            actor.Interacting = false;
            Destroy(initialCamRotation);
            initialCamRotation = null;
        }
    }

    public override void Interact()
    {
       if(initialCamRotation == null)
        {
            initialCamRotation = new GameObject();
            initialCamRotation.transform.rotation = new Quaternion(actor.cam.transform.rotation.x, actor.cam.transform.rotation.y,
            actor.cam.transform.rotation.z, actor.cam.transform.rotation.w);
            //Debug.Log("Initial Cam Rotation: " + initialCamRotation.transform.rotation.eulerAngles.y);
        }
        //Debug.Log("The difference in rotation is: " + (actor.cam.transform.rotation.eulerAngles.y - initialCamRotation.transform.rotation.eulerAngles.y));
        if ((actor.cam.transform.rotation.eulerAngles.y - initialCamRotation.transform.rotation.eulerAngles.y) >= rotationOffset)
        {
            Debug.Log("Rotation is greater than the offset");
            Debug.Log("Force has been applied!");
            doorknob.AddRelativeTorque(Vector3.up * -speed);

            initialCamRotation.transform.rotation = new Quaternion(actor.cam.transform.rotation.x, actor.cam.transform.rotation.y, 
                actor.cam.transform.rotation.z, actor.cam.transform.rotation.w);
        }
        else if ((actor.cam.transform.rotation.eulerAngles.y - initialCamRotation.transform.rotation.eulerAngles.y) <= -rotationOffset)
        {
            Debug.Log("Rotation is less than the offset");
            Debug.Log("Force has been applied!");
            doorknob.AddRelativeTorque(Vector3.up * speed);

            initialCamRotation.transform.rotation = new Quaternion(actor.cam.transform.rotation.x, actor.cam.transform.rotation.y,
                actor.cam.transform.rotation.z, actor.cam.transform.rotation.w);
        }
    }
}
