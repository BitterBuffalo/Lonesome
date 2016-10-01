using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

    public float speed = 1.0f;

    // Use this for initialization
	void Start ()
    {
        //Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis("Vertical") * speed;
        float moveVertical = Input.GetAxis("Horizontal") * speed;

        transform.Translate(moveVertical, 0, moveHorizontal);

        if(Input.GetKeyDown("escape"))
        {
            //Cursor.lockState = CursorLockMode.None;
        }

	}
}
