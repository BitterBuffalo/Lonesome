using UnityEngine;
using System.Collections;

public class DoorITBL : Interactable {

    public GameObject cam;
    public float speed;
    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void Interact()
    {
        rb.AddRelativeTorque(cam.transform.forward * speed);
    }
}
