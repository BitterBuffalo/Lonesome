using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

    public float speed = 1.0f;
    public Camera mainCamera;

    // Use this for initialization
	void Awake ()
    {
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis("Vertical") * speed;
        float moveVertical = Input.GetAxis("Horizontal") * speed;

        transform.Translate(moveVertical, 0, moveHorizontal);
        transform.eulerAngles = new Vector3(0, mainCamera.GetComponent<Transform>().eulerAngles.y, 0);

        if(Input.GetKeyDown("escape"))
        {
            //Cursor.lockState = CursorLockMode.None;
        }

	}
}
